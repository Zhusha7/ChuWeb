# ChuWeb - Professional Portfolio Project

A modern, full-stack web application showcasing my skills in ASP.NET Core and ReactJS. This project demonstrates my ability to build scalable web applications with modern technologies and best practices.

## Project Overview

ChuWeb is a dynamic web application that combines the power of ASP.NET Core for the backend with ReactJS for the frontend. It features a clean, modern UI with real-time updates and responsive design.

## Key Features

- **Full-Stack Architecture**
  - ASP.NET Core Web API backend
  - ReactJS frontend with TypeScript
  - Clean separation of concerns
  - RESTful API design

- **Modern Frontend**
  - ReactJS with TypeScript for type safety
  - Tailwind CSS for responsive design
  - Animate.css for smooth animations
  - Component-based architecture
  - State management with React hooks

- **Backend Excellence**
  - ASP.NET Core Web API
  - Clean architecture principles
  - Entity Framework Core for data access
  - RESTful API endpoints
  - Authentication and authorization

## Technologies Used

### Frontend
- ReactJS with TypeScript
- Tailwind CSS
- Animate.css
- React Router for navigation
- Axios for API communication
- React Query for data fetching

### Backend
- ASP.NET Core 7.0+
- Entity Framework Core
- SQL Server
- JWT Authentication
- Swagger/OpenAPI documentation
- CQRS pattern implementation

## Project Structure

```
ChuWeb/
├── src/
│   ├── ChuWeb.API/           # ASP.NET Core Web API
│   │   ├── Controllers/      # API endpoints
│   │   ├── Models/          # Data models
│   │   ├── Services/        # Business logic
│   │   └── Data/            # Data access layer
│   │
│   └── ChuWeb.Client/       # ReactJS frontend
│       ├── src/
│       │   ├── components/  # Reusable UI components
│       │   ├── pages/       # Page components
│       │   ├── hooks/       # Custom React hooks
│       │   ├── services/    # API services
│       │   └── utils/       # Utility functions
│       └── public/          # Static assets
│
└── tests/                   # Unit and integration tests
```

## Getting Started

### Prerequisites
- .NET 7.0 SDK or later
- Node.js 16.x or later
- SQL Server

### Backend Setup
1. Navigate to the API project
   ```bash
   cd src/ChuWeb.API
   ```

2. Restore dependencies
   ```bash
   dotnet restore
   ```

3. Update the connection string in `appsettings.json`

4. Run database migrations
   ```bash
   dotnet ef database update
   ```

5. Start the API
   ```bash
   dotnet run
   ```

### Frontend Setup
1. Navigate to the client project
   ```bash
   cd src/ChuWeb.Client
   ```

2. Install dependencies
   ```bash
   npm install
   ```

3. Start the development server
   ```bash
   npm run dev
   ```

4. Open your browser and navigate to `http://localhost:5173`

## Development Practices

- **Code Quality**
  - TypeScript for type safety
  - ESLint and Prettier for code formatting
  - Unit tests with Jest and React Testing Library
  - API tests with Swagger UI

- **Git Workflow**
  - Feature branch workflow
  - Pull request reviews
  - Semantic versioning
  - Conventional commits

- **CI/CD**
  - GitHub Actions for automated testing
  - Automated deployment to staging/production
  - Code coverage reporting
  - Security scanning

## Performance Optimization

- Code splitting and lazy loading
- Optimized bundle size
- Caching strategies
- Database query optimization
- API response compression

## Security Features

- JWT authentication
- HTTPS enforcement
- CORS configuration
- Input validation
- XSS protection
- CSRF protection

## Future Enhancements

- [ ] Real-time features with SignalR
- [ ] Progressive Web App (PWA) support
- [ ] Internationalization
- [ ] Dark mode
- [ ] Performance monitoring
- [ ] Analytics integration

## License

MIT License

## Author

Zhusha7 & Catherine Chu
- GitHub: [Zhusha7](https://github.com/Zhusha7)
- LinkedIn: [Zhusha7](https://www.linkedin.com/in/zhusha7)
- Portfolio: In construction...
- Catherine Chu's Twitch: [ChuCatherine](https://www.twitch.tv/chucatherine)
## Acknowledgments

- ASP.NET Core team for the excellent framework
- React team for the amazing library
- All contributors and maintainers of the open-source packages used in this project
- Catherine Chu for being the best streamer, musician, psychotherapist and person I've ever known. 