# Andrew Arcade Apps
A collection of apps for Andrew Arcade.

> More instructions and information will be provided when the project is ready for public contributions.

---

## How to Contribute Your App
To add your app to Andrew Arcade:

1. **Upload Your App Source Code**  
   - Submit the complete source code for your app (your full Unity project) to the apps repository.

2. **Modify the Driver App**  
   - Follow the steps below to add your app to the driver app.

3. **Submit Pull Requests (PRs)**  
   - Once your app is integrated into the driver app, submit a pull request to the apps repository for review.  
   - After approval, build your app and submit a second PR to the main Andrew Arcade repository.

---

## Adding Your App to the Driver App

### 1. Upload Your App Icon
- Navigate to the [Icons folder](https://github.com/AndrewCromar/Andrew-Arcade-Apps/tree/main/Driver/Assets/Resources/Apps/Icons).
- Add your app's icon (image file) to this folder.

### 2. Fix the Icon Resolution
- Ensure your icon is a square image (width and height must be equal).  
- Note your icon's dimensions (e.g., 128x128 pixels).  
- Open the Unity project for the driver app.  
- In Unity, go to **Resources/Apps/Icons** and locate your icon.  
- Select your icon and update the **Pixels Per Unit** setting to match your icon's dimensions (e.g., set **Pixels Per Unit** to `128` if your icon is 128x128).

### 3. Create a JSON File for Your App
- Navigate to the [Apps folder](https://github.com/AndrewCromar/Andrew-Arcade-Apps/tree/main/Driver/Assets/Resources/Apps).
- Create a new JSON file named after your app, using camelCase (e.g., `myApp.json`).

### 4. Edit the JSON File
- Open the JSON file you just created.
- Use the following template and replace the placeholders with your app's details:
  ```json
  {
      "title": "Your App Name",
      "developer": "Your Developer Name",
      "icon": "icons/[your-icon-name]"
  }

- Replace [your-icon-name] with your icon's filename, including its extension.

### 5. Submit a Pull Request for the Driver App
- After completing the above steps, submit a pull request to the apps repository.
- Once approved, your app will be included in the driver app.

---

# What is camelCase?
**camelCase** is a way of naming files, variables, or other elements where:

- The first word is written in lowercase.
- Each subsequent word starts with an uppercase letter, with no spaces or underscores between words.

### Examples:
- myApp
- weatherForecast
- gameSettings

Using camelCase ensures names are readable, concise, and consistent.

---

Follow these steps carefully to contribute your app successfully. Let me know if further clarification is needed!