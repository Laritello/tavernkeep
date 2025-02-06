# Tavernkeep Client

A Vue 3 + TypeScript web application for managing tabletop RPG characters and game sessions.

## Features

Current:

- Character creation and management
- User authentication and role-based access
- Real-time game session updates
- Character stats tracking
- Skill rolls and custom dice rolls
- Condition management
- Speed and armor type handling

Planned:

- Initiative tracker for combat management
- Encounter builder with CR calculation
- Monster database integration
- Combat round automation

## Tech Stack

- Vue 3
- TypeScript
- Vite
- Pinia for state management
- Axios for API communication
- SignalR for real-time updates

## Development Setup

1. Install dependencies:
   `npm install`

2. Start development server:
   `npm run dev`

3. Build for production:
   `npm run build`

4. Run linting:
   `npm run lint`

## IDE Setup

For the best development experience:

1. Use [VSCode](https://code.visualstudio.com/)
2. Install [Volar](https://marketplace.visualstudio.com/items?itemName=Vue.volar)
3. Install [ESLint](https://marketplace.visualstudio.com/items?itemName=dbaeumer.vscode-eslint)
4. Install [Prettier](https://marketplace.visualstudio.com/items?itemName=esbenp.prettier-vscode)
5. Disable Vetur if installed

## Configuration

The API base URL is configured to connect to `http://localhost:5207/api/` by default. Update the `baseURL` in `src/api/axios/AxiosApiClient.ts` to match your backend configuration.

## License

MIT License

Copyright (c) 2024 Tavernkeep

Permission is hereby granted, free of charge, to any person obtaining a copy of this software.
