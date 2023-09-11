# 

- By default, any code that you write in a system runs synchronously on the main thread

- Start is called before the first Awake call just when the frame Script component is enabled. Just like Awake, Start is called only once. However, Awake is called when the object is initialized, regardless of whether the Script component is turned on or off. 
- OnEnable called after Awake and before Start at first time enabled. At next time script enabled, only OnEnable called. 
