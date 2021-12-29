# Shortcuts Grid
###### WPF (.NET 6.0) desktop app that displays 'custom list' grid with links to other programs on the PC or/and run commands. With customizable app names and icons/images.

My inspiration for making this was that Windows 11 start menu doesn't have the grouping apps option. And I start thinking of better way to sort my installed apps so I can access them faster.

Basically it looks something like this:

![img](./screenshots/win11start.jpg "image")![img](./screenshots/win11myapp.jpg "image")  
[Download this example](./examples/Administrative-Tools.zip)

The idea is this exe to display user's group of apps. Copy or build new exe with different name and icon for other group.

## It consist of 2 files:
* exe (show in the screenshots)
* text file (customizable list with programs' path)

To work both files should be in the same folder and have the same names. (it can be seen in the examples)

### Text file types:
* csv (default choice)
* json

### Text file structure:
Every row is one program. It has 3 parameters divided with '|':
* command or file path
* name to display under the program's icon
* image to display (not required) - if left black it'll try to get icon from exe's path; custom image can be assian by path or base64 string

It supports run commands!

It has custom command that this app checks for: 'run' - it displays run dialog.

#### Example:

```
control|Control Panel|iVBORw0KGgoAAAA...
control|Control Panel|C:\Images\Control-Panel-128.png
control|Control Panel|Control-Panel-128.png
```

...more coming...

[a relative link](readme_test.md)

[a relative link](/readme_test.md)

[a relative link](screenshots/readme_test.md)
