# Play Asset Delivery (PAD)

# WHAT - PAD là gì?

- Một giải pháp chia tài nguyên (asset splitting solution) cho Android App Bundle (AAB)
- Mỗi pack tài nguyên (asset pack) sẽ chứa các tài nguyên bổ sung (additional assets): textures, sounds, …
- Google sẽ host và phân phối cho mình, ko cần set up gì thêm bên ngoài hết (quá ngon luôn)

**Đọc thêm:**

- Unity Play Asset Delivery: [Link](https://docs.unity3d.com/Manual/play-asset-delivery.html)

# WHY - Tại sao cần PAD?

- Download size tối đa cho mỗi file APKs tạo ra từ AAB không được vượt quá 150MB (Không có nghĩa là app phải trên 150mb thì mới dùng đâu nha)
- Google Play không hỗ trợ tệp mở rộng (expansion files - OBBs) nữa

**Đọc thêm:**

- Expansion files là gì? [Link](https://developer.android.com/guide/app-bundle/faq#large-apps-games)
- Tại sao lại thay thế PAD cho OBBs?  [Link](https://developer.android.com/guide/app-bundle/faq#what_benefits_does_play_asset_delivery_offer_over_expansion_files_obbs)

# HOW - Cài cắm PAD thế nào?

Thực ra thì các bạn cứ đọc kỹ [doc](https://docs.unity3d.com/Manual/play-asset-delivery.html) của Unity là sẽ làm được 

- Nếu dùng Unity ≥ 2019.4
    - Khuyến khích các bạn dùng [Addressables](https://unity.com/how-to/simplify-your-content-management-addressables#where-can-i-learn-more-about-addressables) để quản lý Assets thay vì cách quản lý [Assetbundles](https://docs.unity3d.com/Manual/AssetBundlesIntro.html) cũ.
    - Mình thường dùng [Addressables Importer](https://github.com/favoyang/unity-addressable-importer) để set up config cho Assets
    - **Các bước cơ bản để set up đọc ở đây**: [Link](https://github.com/Unity-Technologies/Addressables-Sample#advancedplay-asset-delivery)
- Nếu bạn dùng Unity cũ hơn thì đọc ở đây: [Link](https://developer.android.com/guide/playcore/asset-delivery/integrate-unity#assetbundle)

**Note:** Nên hạn chế dùng Resources.Load khi phát triển product ([đọc thêm](https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity6.html))