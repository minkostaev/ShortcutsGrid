# Shortcuts Grid

**WPF (.NET 6.0) desktop app with transparent background that displays 'custom list' grid with links to other programs on the PC or/and run commands. With customizable app names and icons/images.**

My inspiration for making this was that Windows 11 start menu doesn't have the grouping apps option. And I start thinking of better way to sort my installed apps so I can access them faster.

Basically it looks something like this:

![img](./screenshots/win11start.jpg "image")![img](./screenshots/win11myapp.jpg "image")  
[Download this example](./examples/Administrative-Tools.zip)

The idea is this exe to display user's group of apps. Copy or build new exe with different name and icon for other group.

## It consist of 2 files:

* exe (show in the screenshots)
* text file (customizable list with **programs' path** or/and **run commands**)

**In order too work - both files should be in the same folder and have the same names.** [(it can be seen in the examples)](examples)

### Text file structure: list with objects - each object with 3 parameters:

1. run command or file path (it can have argument after the command/path)
2. name to display under the program's icon
3. image to display (not required) - if left black it'll try to get icon from exe's path; custom image can be assign by path or base64 string

*It has custom command, the app checks for: 'run' - it displays run dialog.*

### Text file types:

* csv (default choice)
* json

[csv file content](screenshots/readme_test.md)

[json file content](screenshots/readme_test.md)

#### CSV structure:

Every row is one program. The parameters is divided by '|':

app_path_OR_run_command argument|app_label|image_path_OR_base64_string_of_image

argument is optional!

image_path_OR_base64_string_of_image is optional!

```
C:\Windows\System32\control.exe|Control Panel
|
C:\Windows\System32\control.exe|Control Panel|iVBORw0KGgoAAAA...
control|Control Panel|C:\Images\Control-Panel-128.png
control printers|Devices & Printers|Control-Printers-128.png
//control|Control Panel|Control-Panel-128.png
```

*If it has a row with only '|' it adds empty slot.*

*If row starts '//' it is ignored.*

#### JSON structure:

ExePath is app's path or run command (it can have argument after it)
AppName is app's label
ImgPath is custom image to display - path or base64 string

```
[
	{
		"ExePath": "C:\Windows\System32\control.exe",
		"AppName": "Control Panel",
		"ImgPath": ""
	},
	{
		"ExePath": null,
		"AppName": null,
		"ImgPath": null
	},
	{
		"ExePath": "C:\Windows\System32\control.exe",
		"AppName": "Control Panel",
		"ImgPath": "iVBORw0KGgoAAAA..."
	},
	{
		"ExePath": "control",
		"AppName": "Control Panel",
		"ImgPath": "C:\Images\Control-Panel-128.png"
	},
	{
		"ExePath": "control printers",
		"AppName": "Devices & Printers",
		"ImgPath": "Control-Printers-128.png"
	}
]
```

*If it has a object with only 'null' fields it adds empty slot.*

