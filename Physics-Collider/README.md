

- isKenematic : Sẽ tắt các tính chất vật lý đi (VD Trọng lực, lực đẩy, ..) và sẽ dùng để mình tự custom viết di chuyển, tương tác vật lý bằng script. Tuy nhiên isKenematic vẫn có tính chất vật lý cơ bản của Collider, ngăn chặn các Object vật lý khác đi xuyên qua.
- Đối với những Object nào mà ko di chuyển, thì ta nên tích vào là Static sẽ giúp tối ưu hơn trong việc render, tối ưu Batchs.

