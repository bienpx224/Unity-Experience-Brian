import { Injectable } from '@nestjs/common';
import { Bot, InlineKeyboard } from 'grammy';
import telegramConfig from 'config/telegram.config';
import { IapConfig } from '../../config/game/iap.config';
import { PaymentService } from '../payment/payment.service';
import appConfig from '../../config/app.config';

@Injectable()
export class BotGameTelegram {
  private bot: Bot;

  constructor(private readonly paymentService: PaymentService) {
    this.bot = new Bot(telegramConfig().telegramBotGameToken);
    this.gamePlay();
    this.createIap();
    this.preCheckoutQuery();
    this.successfulPayment();
    this.refundStarPayment();
    this.bot.start();
  }

  private gamePlay() {
    this.bot.command(['play', 'start'], async (msg: any) => {
      const keyboard = new InlineKeyboard().url(
        `🕹️ Play Game`,
        'https://t.me/TeleMiniAppBrianBot/coinmaster',
      );
      await msg.reply('👏Welcome to the game', {
        reply_markup: keyboard,
      });
    });
  }

  private createIap() {
    this.bot.command('create', async (msg) => {
      if (
        telegramConfig().telegramBotNotification.mentions.includes(
          msg.message.from.id,
        )
      ) {
        const chatId = msg.chat.id;
        for (const iap of IapConfig) {
          const prices = [
            {
              label: iap.title,
              amount: iap.priceStar,
            },
          ];
          this.bot.api
            .createInvoiceLink(
              iap.title,
              iap.description,
              iap.payload,
              iap.providerToken,
              iap.currency,
              prices,
            )
            .then((resp) => {
              console.log('Invoice Link', resp);
              this.bot.api.sendMessage(
                chatId,
                'Invoice created link : ' + resp,
              );
            });
        }
      }
    });
  }

  private preCheckoutQuery() {
    this.bot.on('pre_checkout_query', (ctx) => {
      this.bot.api
        .answerPreCheckoutQuery(ctx.update.pre_checkout_query.id, true)
        .catch(() => {
          console.error('answerPreCheckoutQuery failed');
        });
    });
  }

  private successfulPayment() {
    this.bot.on('message:successful_payment', async (msg) => {
      const chatId = msg.chat.id;

      await this.paymentService.create(
        {
          userId: msg.update.message.from.id.toString(),
          currency: msg.update.message.successful_payment.currency,
          timestamp: msg.update.message.date,
          invoicePayload: msg.update.message.successful_payment.invoice_payload,
          totalAmount: msg.update.message.successful_payment.total_amount,
          providerPaymentChargeId:
            msg.update.message.successful_payment.provider_payment_charge_id,
          telegramPaymentChargeId:
            msg.update.message.successful_payment.telegram_payment_charge_id,
        },
        true,
      );
      let rsText = 'Thank you for purchasing a item from our Store! \n \n ';
      rsText += `transactionID: ${msg.update.message.successful_payment.telegram_payment_charge_id}`;
      await this.bot.api.sendMessage(chatId, rsText);
    });
  }

  private refundStarPayment() {
    if (appConfig().env === 'product') return;
    this.bot.command('refund', async (msg) => {
      const chatId = msg.chat.id;
      const arrayString = msg.message.text.split(' ');
      const telegramPaymentChargeId = arrayString.find(
        (string) => string !== '/refund' && string !== '',
      );
      try {
        const result = await this.bot.api.refundStarPayment(
          msg.message.from.id,
          telegramPaymentChargeId,
        );
        if (result) {
          await this.bot.api.sendMessage(chatId, 'Refund successful');
        } else {
          await this.bot.api.sendMessage(chatId, 'Refund failed');
        }
      } catch (e) {
        await this.bot.api.sendMessage(chatId, 'Refund failed');
      }
    });
  }
}
