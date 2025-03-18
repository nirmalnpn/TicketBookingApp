TicketBookingApp
│
├── Controllers                     # Contains all the API and MVC controllers
│   ├── AdventureController.cs
│   ├── BookingController.cs
│   ├── GuideController.cs
│   ├── PhotoController.cs
│   └── UserController.cs
│
├── Data                             # Contains repositories for data access (Dapper)
│   ├── AdventureRepository.cs
│   ├── BookingRepository.cs
│   ├── GuideRepository.cs
│   ├── PhotoRepository.cs
│   ├── UserRepository.cs
│   └── DapperContext.cs            # (If needed, handles the shared logic for Dapper connections)
│
├── Models                           # Contains model classes representing your data
│   ├── Adventure.cs
│   ├── Booking.cs
│   ├── Guide.cs
│   ├── Photo.cs
│   ├── User.cs
│   └── JwtSettings.cs               # Contains JWT settings if you are using JWT for authentication
│
├── Pages                            # Contains Razor Pages
│   ├── Account                      # For login, registration, etc.
│   │   ├── Login.cshtml
│   │   ├── Login.cshtml.cs
│   │   ├── Register.cshtml
│   │   └── Register.cshtml.cs
│   ├── Adventure                    # For displaying and booking adventures
│   │   ├── Index.cshtml
│   │   ├── Index.cshtml.cs
│   │   └── Details.cshtml           # (For Adventure Details Page)
│   ├── Booking                      # For handling bookings
│   │   ├── Index.cshtml
│   │   ├── Index.cshtml.cs
│   │   └── Success.cshtml
│   ├── Photo                        # For uploading and viewing photos
│   │   └── Index.cshtml
│   ├── Index.cshtml                 # The homepage of the application
│   └── _Layout.cshtml               # Shared layout file
│
├── Services                         # Contains additional services like JWT generation
│   ├── JwtService.cs                # Logic for JWT generation and validation
│
├── wwwroot                          # Static files like images, CSS, JS
│   ├── css
│   │   └── site.css                 # Custom styles for your app
│   ├── js
│   │   └── site.js                  # Custom JavaScript for your app
│   └── images                       # For images related to adventures, guides, etc.
│
├── appsettings.json                 # Configuration file for app settings (e.g., connection strings, JWT keys)
├── Program.cs                       # The entry point for the application (Handles services and middleware)
└── Startup.cs                       # (This file is not used since all setup is in Program.cs)
