# Weather Subscriber Technical Support

Thank you for using Weather Subscriber! If you encounter issues or need assistance, please use the following resources.

## 1. Common Issues & Solutions

### API or UI Not Starting
- Ensure you have the .NET 9 SDK installed.
- Run `dotnet run` in both `WeatherAPI` and `WeatherUI` folders.
- Check for port conflicts (default: 5000 for API, 5096 for UI).

### Email Notifications Not Working
- Verify SMTP settings in `WeatherAPI/appsettings.json`.
- Make sure your SMTP credentials are correct and the account allows SMTP access.
- Check your spam folder for missing emails.

### Weather Data Not Loading
- Confirm your OpenWeatherMap API key is valid and not expired.
- Check your internet connection.

### Static Images Not Displaying
- Place images in `WeatherUI/wwwroot/images/`.
- Reference images as `images/filename` in your Blazor components.

## 2. Getting Help
- Review the `README.md` and `USER_MANUAL.md` for setup and usage instructions.
- For technical questions, bug reports, or feature requests, open an issue at:
  [https://github.com/nicobarbosa21/Weather-Subscriber](https://github.com/nicobarbosa21/Weather-Subscriber)
- For direct support, email: nicolasbarbosa250@gmail.com

## 3. Contact
- **Email:** nicolasbarbosa250@gmail.com
- **GitHub Issues:** [Weather-Subscriber Issues](https://github.com/nicobarbosa21/Weather-Subscriber/issues)

We aim to respond to support requests within 2 business days.
