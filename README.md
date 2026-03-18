# 🍽️ ApiYummy - Intelligent Restaurant Management System

ApiYummy is a modern, full-stack restaurant management and reservation system built with ASP.NET Core 8.0. The project stands out by integrating advanced AI capabilities (Natural Language Processing and Large Language Models) with real-time communication tools.

---

## 🚀 Architectural Overview

The project follows a decoupled **API-First Architecture** consisting of two main layers:

### 1. Web.API Layer (Backend)
- **RESTful Services:** Handles all CRUD operations (Create, Read, Update, Delete) for menus, chefs, reservations, and messages.
- **Data Access:** Utilizes Entity Framework Core with a code-first approach for database management and migrations.
- **DTO Pattern:** Direct database entity exposure is avoided. All data transfer is managed through Data Transfer Objects (DTOs).
- **AutoMapper:** Integrated for seamless object-to-object mapping between entities and DTOs.
- **Validation:** Fundamental validation rules implemented to ensure data integrity.

### 2. WebUI Layer (Frontend)
- **API Consumption:** Backend services are consumed using HttpClient Factory.
- **Component-Based UI:** The interface is built with ViewComponents, making the UI modular, reusable, and easy to maintain.
- **Modern Dashboard:** A sleek, responsive admin panel designed for efficient restaurant operations.

---

## 🤖 Advanced AI & Real-Time Engine

The core innovation of ApiYummy lies in its AI-driven features:

### ⚡ Real-Time Chat with Groq Cloud (Llama/Mixtral) & SignalR
- **Instant AI Streaming:** Unlike traditional request-response cycles, the chat system uses SignalR to stream AI responses bit-by-bit.
- **Groq Integration:** Leverages Groq Cloud LLMs for ultra-fast, intelligent conversations.
- **Streaming Logic:** Implemented Delta, Choice, and Chat Stream Chunk classes to handle live token streams, creating a seamless "ChatGPT-style" typing experience.

### 🧠 Hugging Face Intelligence (NLP)
- **Neural Translation:** Uses the Helsinki-NLP model to translate Turkish customer messages into English for global processing.
- **Safety & Moderation:** Automated Toxic Content Detection using BERT-based models to filter inappropriate messages and ensure a professional environment.

### 🌍 Global Recipe Generator (Groq AI)
- **Culinary Knowledge:** A dedicated module that uses Groq AI to generate authentic recipes from world cuisines (Peruvian, Italian, Turkish, etc.) for the chef's inspiration.

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| **Framework** | ASP.NET Core 8.0 (Web API & MVC) |
| **Real-Time** | SignalR |
| **AI Ecosystem** | Groq Cloud API, Hugging Face NLP Models |
| **ORM** | Entity Framework Core (SQL Server) |
| **Mapping** | AutoMapper |
| **UI Architecture** | Bootstrap 5, FontAwesome, ViewComponents, JavaScript (ES6) |

---

## 📸 Screenshots

<img width="1920" height="717" alt="image" src="https://github.com/user-attachments/assets/5431e9a1-1986-4c4c-bf95-94057b2a0502" />
<img width="1903" height="792" alt="image" src="https://github.com/user-attachments/assets/17c00e7a-460f-4dea-9290-97279cb95c05" />

