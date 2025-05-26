# Weather Subscriber

A modern, user-friendly Blazor application for weather forecasting and email notifications. This project consists of two main components:

- **WeatherUI**: A Blazor WebAssembly frontend with a beautiful, responsive interface.
- **WeatherAPI**: An ASP.NET Core Web API backend for weather data and email notifications.

## Features
- View 5-day weather forecasts for any city
- Subscribe to daily weather email notifications
- Manage subscribers (add/remove)
- Modern, attractive UI with Bootstrap and custom CSS

## Installation

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Node.js](https://nodejs.org/) (for frontend static assets, optional)

### Clone the Repository
```
git clone https://github.com/nicobarbosa21/Weather-Subscriber.git
cd Weather-Subscriber
```

### Backend Setup (WeatherAPI)
1. Navigate to the `WeatherAPI` folder:
   ```
   cd WeatherAPI
   ```
2. Configure your API keys and SMTP settings in `appsettings.json`:
   - `Weather:ApiKey`: [Get an OpenWeatherMap API key](https://openweathermap.org/api)
   - `Smtp`: Set your SMTP host, port, user, and password for email notifications.
3. Run the API:
   ```
   dotnet run
   ```
   The API will be available at `http://localhost:5000` by default.

### Frontend Setup (WeatherUI)
1. Open a new terminal and navigate to the `WeatherUI` folder:
   ```
   cd WeatherUI
   ```
2. Run the Blazor WebAssembly app:
   ```
   dotnet run
   ```
   The UI will be available at `http://localhost:5096` by default.

### Static Files & Images
- Place images in `WeatherUI/wwwroot/images/` and reference them as `images/filename` in your components.

## Usage
- Access the Home page at `/` for a welcome message and image.
- Use the sidebar to navigate to Notifier and Weather pages.
- Add subscribers and send weather notifications from the Notifier page.

## Contributing
Pull requests are welcome! For major changes, please open an issue first.

## License
MIT

---
For questions or support, see `TECH_SUPPORT.md` or contact nicolasbarbosa250@gmail.com.
