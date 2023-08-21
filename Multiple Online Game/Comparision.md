# Comparision between PUN and st: 

## About Unity Networking : 
- Price : 500 CCU ~ 50k MAU
- NOT Support WebGL, Relay, Lobby (use them in Unity Game Services..)
- 

## About PUN - Info and Pricing/Costs: 
* PUN PRICING : [Link](https://www.photonengine.com/pun/pricing)
- Dedicated Server
- Support Matchmaking, Room, Lobby, Relay, WebGL (WSS), Master Server, Custom Authentication, Webhooks and WebRPC.
- With PUN, if the Master Client quits, another actor -if available- will become the new Master Client. And so on, until there are no more clients.
- Max in room : 32 players. 
- The 500msg/sec/room is a soft limit highly. Let's assume: if player send msg F = 10 (send rate) and N player = 10 ==> Number msg = N * N * F =  100 * 10 = 1000.
- Photon includes 3GB per CCU .

- Free : 20 CCU : Online users in server at the same time.
- Photon Public Cloud for Gaming : CCU : 100 CCU (95$ one time for 12 months), 500 CCU (95$/month), 1000 CCU (185$/mon), 2000 CCU (370$/mon). 
- Photon PREMIUM Cloud for gaming : Per CCU (0.29$) - Invoiced based on monthly usage. UP to 50.000 CCU

- Sample Month: EU peak 420 CCU, US peak 230 CCU, Other regions 0 CCU — CCU total = 420 + 230 = 650, matching 1000 CCU subscription plan.
- One-Time Plans : 
One-time plans apply to one application and for 12 months, no recurring fees. You can add 100 CCU to any other existing monthly subscription plan, e.g. 100 + 500 = 600 CCU in total. Can be applied to an app only once at a time.
- 14 regions on the world.

- Monthly Active Users (MAU)
We assume, as rule of thumb and based on our experience, 1 CCU to match 20 daily active users (DAU). Each DAU translates to 20 monthly actives (MAU).
( Quy đổi : 1 CCU tương ứng với 20 user active hằng ngày, mỗi User active hàng ngày tương đương với 20 active hàng tháng). 
=> VD : Có 500 CCU thì ~ 200k MAU : Dùng gói 95$/mon
- Traffic Calculation
The included traffic depends on your subscription plan. All incoming and outgoing messages to the Photon Cloud servers count towards the traffic limit. Overage fees apply to traffic exceeding that limit - see table above.

    + Sample: 1000 CCU plan * 3 GB incl. per CCU = 3 TB included traffic.

- Hacking / Cheating : 
- Pure client side
- Server Side Monitoring 
- Full Server Authority

# About Bolt : 
- Host Client : 
With Bolt, on the other hand, the server is "static"; the "client" that starts up the game and choose to become the server, will stay as the server. If the host client (server) quits, none of the other clients will become a server (and all of them will be disconnected, of course.)