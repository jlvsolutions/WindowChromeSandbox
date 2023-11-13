# WindowChromeSandbox
A Sandbox for exploring different WindowChrome usage techniques in WPF applications to create custom title bars based on ideas from different authors.


## CustomTitleBarWindow
A WPF application using techniques published by DAVID RICKARD on his [tech blog](https://engy.us/blog/2020/01/01/implementing-a-custom-window-title-bar-in-wpf/).  This project is kept simple by not employing binding/MVVM patters and instead uses the code behind for interactions.
It also uses user32.dll DLL imports to correct pixes being cut off while maximized, and to make the application per-moniter DPI aware.

## CustomTitleBarWindowMVVM
This implementation of using WindowChrome in order to get custom behavior and title bar is based upon a [YouTube](https://www.youtube.com/watch?v=TDOxHx-AMqQ) video by AngelSix.  
