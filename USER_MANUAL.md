# Weather Subscriber User Manual

Welcome to the Weather Subscriber application! This guide will help you get started and make the most of the app’s features.

## Table of Contents
1. [Overview](#overview)
2. [Navigation](#navigation)
3. [Weather Forecast](#weather-forecast)
4. [Notifier & Subscriptions](#notifier--subscriptions)
5. [Managing Subscribers](#managing-subscribers)
6. [Troubleshooting](#troubleshooting)

---

## Overview
Weather Subscriber is a web application that allows you to:
- View 5-day weather forecasts for any city
- Subscribe to daily weather email notifications
- Manage your subscriptions easily

## Navigation
- **Home**: Welcome page with a project overview and image.
- **Notifier**: Manage email subscriptions and send weather notifications.
- **Weather**: View and search for weather forecasts by city.

Use the sidebar on the left to switch between sections. Icons help identify each section:
- 🏠 Home
- ✉️ Notifier
- 🌦️ Weather

## Weather Forecast
1. Go to the **Weather** page.
2. Select a city from the dropdown or enter a city name.
3. View the 5-day forecast displayed in a modern, easy-to-read table.

## Notifier & Subscriptions
1. Go to the **Notifier** page.
2. Add a new subscriber by entering Name, Email, and City.
3. Click **Add** to save the subscriber.
4. To send weather notifications to all subscribers, click **Send Weather**.

## Managing Subscribers
- Subscribers are listed in a table.
- To remove a subscriber, click the **Delete** button next to their entry.

## Troubleshooting
- **Images not loading?** Ensure your images are in `WeatherUI/wwwroot/images/` and referenced as `images/filename`.
- **Emails not sending?** Check your SMTP settings in `WeatherAPI/appsettings.json`.
- **API not responding?** Make sure the backend is running on `http://localhost:5000`.

For further help, see `TECH_SUPPORT.md` or contact nicolasbarbosa250@gmail.com.
