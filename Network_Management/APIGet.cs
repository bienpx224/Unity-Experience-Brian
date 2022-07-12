using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class APIGet
{
	public static string APIGetAllIsland()
	{
		return string.Format("{0}islands/my/islands", GameConstants.HOST);
	}

	public static string APIGetIslandById(string islandId)
	{
		return string.Format("{0}islands/my/islands/{1}", GameConstants.HOST, islandId);
	}


	public static string APIGetUserProfile()
	{
		return string.Format("{0}islands/my/users/profile", GameConstants.HOST);
	}
	public static string APIGetUserLevelUpDetail(int level = -1)
	{
		if (level == -1)
		{
			return string.Format("{0}islands/my/users/levelup", GameConstants.HOST);
		}
		else
		{
			return string.Format("{0}islands/my/users/levelup?level={1}", GameConstants.HOST, level);
		}
	}
	public static string APIGetFeeLevelUpBunihouse(string bunihouseId)
	{
		return string.Format("{0}islands/my/buni-houses/{1}/levelup", GameConstants.HOST, bunihouseId);
	}
	public static string APIGetFeeLevelUpHatchery(int id)
	{
		return string.Format("{1}islands/my/hatchery/{0}/levelup", id, GameConstants.HOST);
	}
	public static string APIGetFeeSpeedUpHatchery(int id)
	{
		return string.Format("{1}islands/my/hatchery/{0}/speedup", id, GameConstants.HOST);
	}
	public static string APIGetAllFarm()
	{
		return string.Format("{0}islands/my/farms", GameConstants.HOST);
	}


	public static string APIGetAllBuniHouse()
	{
		return string.Format("{0}islands/my/buni-houses", GameConstants.HOST);
	}


	public static string APIGetExpandableSlot(string islandId)
	{
		return string.Format("{1}islands/my/islands/{0}/expandable-slots", islandId, GameConstants.HOST);
	}

	public static string APIGetFarmById(string id)
	{
		return string.Format("{1}islands/my/farms/{0}", id, GameConstants.HOST);
	}

	public static string APIGetFeeUpgradeBuniHouse(string buniHouseId)
	{
		return string.Format("{0}islands/my/buni-houses/{1}/levelup", GameConstants.HOST, buniHouseId);
	}
	public static string APIGetFeeUpgradeStorage()
	{
		return string.Format("{0}islands/my/storage/levelup", GameConstants.HOST);
	}

	public static string APIGetFeeSpeedUpStorage()
	{
		return string.Format("{0}islands/my/storage/speedup", GameConstants.HOST);
	}

	public static string APIGetStorageData()
	{
		return string.Format("{0}islands/my/storage", GameConstants.HOST);
	}
	public static string APIGetTrainingCenterById(string id)
	{
		return string.Format("{1}islands/my/training-centers/{0}", id, GameConstants.HOST);
	}
	public static string APIGetMentoringById(string id)
	{
		return string.Format("{1}islands/my/mentoring-houses/{0}", id, GameConstants.HOST);
	}



	public static string APIGetAllTrainingCenter()
	{
		return string.Format("{0}islands/my/training-centers/", GameConstants.HOST);
	}

	public static string APIGetBunihouseById(string id)
	{
		return string.Format("{1}islands/my/buni-houses/{0}", id, GameConstants.HOST);
	}

	public static string APIGetBuniAvoidHalvingFeeAll()
	{
		return string.Format("{0}islands/my/bunicorns/avoid-all-halving", GameConstants.HOST);
	}
	public static string APIGetTrainerAvoidHalvingFeeAll()
	{
		return string.Format("{0}islands/my/trainers/avoid-all-halving", GameConstants.HOST);
	}


	public static string APIGetBreedingHouseById(string id)
	{
		return string.Format("{1}islands/my/breeding-houses/{0}", id, GameConstants.HOST);
	}

	public static string APIGetObstacleById(string id)
	{
		return string.Format("{1}islands/my/islands/{0}/obstacles", id, GameConstants.HOST);
	}
	public static string APIGetTempleById(string id)
	{
		return string.Format("{1}islands/my/temples/{0}", id, GameConstants.HOST);
	}
	public static string APIGetHatcheryById(string id)
	{
		return string.Format("{1}islands/my/hatchery/{0}", id, GameConstants.HOST);
	}
	public static string APIGetAllTempleById()
	{
		return string.Format("{0}islands/my/temples", GameConstants.HOST);
	}
	public static string APIGetTempleSpeedUpFee(string templeId)
	{
		return string.Format("{0}islands/my/temples/{1}/speedup", GameConstants.HOST, templeId);
	}

	public static string APIGetBunihouseInfoHarvest(string buniHouseId)
	{
		return string.Format("{0}islands/my/buni-houses/{1}/harvest", GameConstants.HOST, buniHouseId);
	}

	public static string APIGetAllBreedingHouse()
	{
		return string.Format("{0}islands/my/breeding-houses", GameConstants.HOST);
	}
	public static string APIGetAllTrainers()
	{
		return string.Format("{0}islands/v2/my/trainers/in-game?limit=200", GameConstants.HOST);
	}

	public static string APIGetBunicorns()
	{
		string url = string.Format("{0}islands/v2/my/bunicorns/in-game?limit=200", GameConstants.HOST);
		return url;
	}
	
	public static string APIGetSkipEnemyPrice()
	{
		string url = string.Format("{0}islands/my/battles/skip-enemy", GameConstants.HOST);
		return url;
	}

	public static string APIGetListBunicornDetails(List<string> ids)
	{
		string url = string.Format("{0}islands/my/bunicorns/list-details?bunicornIds=", GameConstants.HOST);
		string idsUrl = "";
		while (ids.Count > 0)
		{
			idsUrl += ids + ",";
		}
		url += UnityWebRequest.EscapeURL(idsUrl);
		return url;

	}

	public static string APIGetBunicornSkillTraining(string buniCornId)
	{
		string url = string.Format("{0}islands/my/training-centers/{1}/training-skills", GameConstants.HOST, buniCornId);
		return url;
	}
	public static string APIGetListBunicornForBattle()
	{
		string url = string.Format("{0}islands/my/bunicorns/for-function?limit=200&function=GetBunicornsForBattle", GameConstants.HOST);
		return url;
	}

	public static string APIGetBunicornDefeated()
	{
		string url = string.Format("{0}islands/my/users/defeated-bunicorn", GameConstants.HOST);
		return url;
	}


	public static string APIGetTeam()
	{
		string url = string.Format("{0}islands/my/teams", GameConstants.HOST);
		return url;
	}


	public static string APIGetTeamRevives(int[] ids)
	{
		string idsStr = ids[0].ToString();
		for (int i = 1; i < ids.Length; i++)
		{
			idsStr += "," + ids[i];
		}
		string url = string.Format("{0}islands/my/bunicorns/next-battle-times?bunicornIds={1}", GameConstants.HOST, idsStr);
		return url;

	}

	public static string APIGetDefendTeam()
	{
		string url = string.Format("{0}islands/my/team-protect-gold", GameConstants.HOST);
		return url;
	}

	public static string APIGetStealTeam()
	{
		string url = string.Format("{0}islands/my/team-steal-gold", GameConstants.HOST);
		return url;
	}


	public static string APIGetListEnhance(string buniCornId)
	{
		string url = string.Format("{0}islands/my/bunicorns/for-function?function=GetBunicornsCanBurnForEnhance&enhanced={1}", GameConstants.HOST, buniCornId);
		return url;
	}
	public static string APIGetListEnhanceWithPrice(string buniCornId)
	{
		string url = string.Format("{0}islands/my/bunicorns/for-function?function=GetBunicornsCanBurnForEnhance&limit=200&enhanced={1}", GameConstants.HOST, buniCornId);
		return url;
	}


	public static string APIGetListFusion(string trainerId)
	{
		string url = string.Format("{0}islands/my/trainers/for-function?function=GetTrainersCanBurnForFusion&limit=200&fused={1}", GameConstants.HOST, trainerId);
		return url;
	}
	public static string APIGetListTrainerMentor(int levelFrom = 1, int levelTo = 300, string element = "", int fromMentoringCount = 0, int toMentoringCount = 5, int limit = 200)
	{
		string url = string.Format("{0}islands/my/trainers/for-function?function=GetTrainersMentor", GameConstants.HOST);
		url += string.Format("&limit={0}&levelFrom={1}&levelTo={2}&fromMentoringCount={3}&toMentoringCount={4}", limit, levelFrom, levelTo, fromMentoringCount, toMentoringCount);
		if (element != null && element.Length > 0)
		{
			url += string.Format("&element={0}", element);
		}
		return url;
	}
	public static string APIGetListAllTrainer(int levelFrom = 1, int levelTo = 300, string element = "", int limit = 200)
	{
		string url = string.Format("{0}islands/v2/my/trainers/in-game?", GameConstants.HOST);
		url += string.Format("limit={0}&levelFrom={1}&levelTo={2}", limit, levelFrom, levelTo);
		if (element != null && element.Length > 0)
		{
			url += string.Format("&element={0}", element);
		}
		return url;
	}
	public static string APIGetListAllBunicorn(int levelFrom = 1, int levelTo = 300, List<string> element = default(List<string>), int starsFrom = 1, int starsTo = 5, int limit = 200)
	{
		string url = string.Format("{0}islands/v2/my/bunicorns/in-game?", GameConstants.HOST);
		url += string.Format("limit={0}&levelFrom={1}&levelTo={2}&starsFrom={3}&starsTo={4}", limit, levelFrom, levelTo, starsFrom, starsTo);
		string elementStr = "";
		if (element != null && element.Count > 0)
		{
			for (int i = 0; i <= element.Count - 1; i++)
			{
				if (elementStr.Length == 0)
				{
					elementStr += "&element=" + element[i];
				}
				else
				{
					elementStr += "%2C" + element[i];
				}
			}
			url += string.Format("{0}", elementStr);
		}
		return url;
	}

	public static string APIGetPriceEnhance(string buniCornId, int[] burnId)
	{
		string str = string.Format("burnIds={0}", burnId[0]);
		for (int i = 1; i < burnId.Length; i++)
		{
			str += string.Format("&burnIds={0}", burnId[i]);
		}
		string url = string.Format("{0}bunicorns/public/bunicorns/{1}/enhance-fee?{2}", GameConstants.HOST, buniCornId, str);
		return url;
	}

	public static string APIGetPriceFusion(string trainerId)
	{
		
		string url = string.Format("{0}trainers/public/trainers/{1}/fuse-fee", GameConstants.HOST, trainerId);
		return url;
	}

	public static string APIGetBuildingConfig()
	{

		string url = string.Format("{0}islands/my/shop/buildings", GameConstants.HOST);
		return url;
	}



	public static string APIGetFeeUnlockIsland(string islandId)
	{
		string url = string.Format("{1}islands/my/islands/{0}/unlock-island", islandId, GameConstants.HOST);
		return url;

	}
	public static string APIGetBreedingAvailable()
	{
		string url = string.Format("{0}islands/my/bunicorns/for-function?function=GetBunicornsCanBreeding", GameConstants.HOST);
		return url;

	}
	public static string APIGetBreedingSpeedFee(string breedingId)
	{
		string url = string.Format("{0}islands/my/breeding-houses/{1}/speedup", GameConstants.HOST, breedingId);
		return url;

	}

	public static string APIGetBreedingInfo(string breedingId, string bunicornLeftId, string bunicornRightId)
	{
		string url = string.Format("{0}islands/my/breeding-houses/{1}/breeding-fee?bunicornIds={2}%2C{3}", GameConstants.HOST, breedingId, bunicornLeftId, bunicornRightId);
		return url;

	}


	public static string APIGetBreedingRateInfo(string breedingId)
	{
		string url = string.Format("{0}islands/my/breeding-houses/{1}/speedup", GameConstants.HOST, breedingId);
		return url;
	}
	public static string APIGetBuniFeedInfo(string bunicornId)
	{
		string url = string.Format("{0}bunicorns/my/bunicorns/{1}/requirement-food", GameConstants.HOST, bunicornId);
		return url;
	}

	public static string APIGetOverviewToday()
	{
		return string.Format("{0}islands/public/dashboard/overview-today", GameConstants.HOST);
	}
	public static string APIGetFindAllMyStatistics()
	{
		string today = DateTime.Now.ToString("yyyy-MM-dd");
		string yesterday = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
		string page = "1", limit = "200";
		string url = string.Format("{0}islands/my/statistics?page={1}&limit={2}&fromDate={3}&toDate={4}", GameConstants.HOST, page, limit, today, today);
		return url;
	}

	public static string APIGetAllConfig()
	{
		return string.Format("{0}islands/public/game-config",
		GameConstants.HOST);
	}

	public static string APIGetFindAllMyHatchery()
	{
		string page = "1", limit = "200";
		return string.Format("{0}islands/my/hatchery?page={1}&limit={2}",
		GameConstants.HOST, page, limit);
	}
	public static string APIGetFindAllMyMentoring()
	{
		return string.Format("{0}islands/my/mentoring-houses", GameConstants.HOST);
	}
	public static string APIGetHatcheryEggs(int id)
	{
		return string.Format("{1}islands/my/hatchery/{0}/eggs", id, GameConstants.HOST);
	}
	public static string APIGetMyEggs(bool canAddToHatchery = true, string filterElement = "", int starsFrom = 0, int starsTo = 5)
	{
		string page = "1", limit = "200";
		string url = string.Format("{0}islands/my/eggs?page={1}&limit={2}", GameConstants.HOST, page, limit);
		if (canAddToHatchery)
		{
			url += "&canAddToHatchery=true";
		}
		else
		{
			url += "&canAddToHatchery=false";
		}
		if (filterElement != null && filterElement.Length > 0)
		{
			url += "&element=" + filterElement;
		}
		url += "&starsFrom=" + starsFrom;
		url += "&starsTo=" + starsTo;
		return url;
	}
	public static string APIGetFeeSpeedupSinggleEgg(int eggId)
	{
		return string.Format("{1}islands/my/eggs/{0}/speedup", eggId, GameConstants.HOST);
	}
	public static string APIGetBunicornById(int id)
	{
		string url = string.Format("{0}islands/my/bunicorns/{1}", GameConstants.HOST, id);
		return url;
	}

	public static string APIGetTrainerById(int id)
	{
		string url = string.Format("{0}islands/my/trainers/{1}", GameConstants.HOST, id);
		return url;
	}

	public static string APIGetTrainingCenterSpeedFee(string id)
	{
		return string.Format("{1}islands/my/training-centers/{0}/speedup", id, GameConstants.HOST);
	}
	public static string APIGetTrainingSkillSpeedFee(string id)
	{
		return string.Format("{1}islands/my/training-centers/{0}/speedup-train", id, GameConstants.HOST);
	}

	public static string APIGetStolenLog(int page)
	{
		return string.Format("{1}islands/my/storage/stolen-logs?limit=50", page, GameConstants.HOST);
	}

	public static string APIGetStealLog(int page)
	{
		return string.Format("{1}islands/my/storage/stealing-logs?limit=50", page, GameConstants.HOST);
	}

	public static string APIGetBattleReplay(string battleId)
	{
		return string.Format("{1}islands/my/storage/battle-logs/{0}", battleId, GameConstants.HOST);
	}

	public static string APIGetBunicornByIds(int[] ids)
	{
		string str = "";
		for (int i = 0; i < ids.Length; i++)
		{
			str += ids[i] + ",";
		}
		return string.Format("{1}islands/public/bunicorns?bunicornIds={0}", str, GameConstants.HOST);
	}

	public static string APIGetTrainerByIds(int[] ids)
	{
		string str = "";
		for (int i = 0; i < ids.Length; i++)
		{
			str += ids[i] + ",";
		}
		return string.Format("{1}islands/public/trainers?trainerIds={0}", str, GameConstants.HOST);
	}

	public static string APIGetFeeSpeedUpFarm(string farmId)
	{
		
		return string.Format("{1}islands/my/farms/{0}/speedup", farmId, GameConstants.HOST);
	}

	//public static string APIGetBattleReplay(string battleId)
	//{
	//	return string.Format("{1}trainers/public/trainers/1/fuse-fee", battleId, GameConstants.HOST);
	//}

	public static string APIGetListBunicornCanTrain()
	{
		string url = string.Format("{0}islands/my/bunicorns/for-function?function=GetBunicornsToTrain&limit=200", GameConstants.HOST);
		return url;
	}

}
