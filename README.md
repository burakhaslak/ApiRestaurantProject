# 🍽️ ApiYummy - Intelligent Restaurant Management System

ApiYummy is a modern, full-stack restaurant management and reservation system built with ASP.NET Core 9.0. The project stands out by integrating advanced AI capabilities (Natural Language Processing and Large Language Models) with real-time communication tools.

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
| **Framework** | ASP.NET Core 9.0 (Web API & MVC) |
| **Real-Time** | SignalR |
| **AI Ecosystem** | Groq Cloud API, Hugging Face NLP Models |
| **ORM** | Entity Framework Core (SQL Server) |
| **Mapping** | AutoMapper |
| **UI Architecture** | Bootstrap 5, FontAwesome, ViewComponents, JavaScript (ES6) |

---

## 📸 Screenshots

### Home Page
<img width="1908" height="742" alt="image" src="https://github.com/user-attachments/assets/6c8c1e31-8faa-451a-8b0b-0d6db824ecff" />
<img width="1903" height="825" alt="image" src="https://github.com/user-attachments/assets/7e506243-f0b0-4fb6-8d7a-555a32fcb861" />
<img width="1903" height="469" alt="image" src="https://github.com/user-attachments/assets/17505601-d897-4686-b9e2-5e994777e7f7" />
<img width="1911" height="396" alt="image" src="https://github.com/user-attachments/assets/99004999-132f-4749-b88c-aa6d9a8554da" />
<img width="1907" height="824" alt="image" src="https://github.com/user-attachments/assets/f3ba1abb-0537-408e-9a78-e6e0f72911b3" />
<img width="1910" height="633" alt="image" src="https://github.com/user-attachments/assets/32e36776-e7bb-42e6-a46c-066ddac5bec8" />
<img width="1906" height="746" alt="image" src="https://github.com/user-attachments/assets/d51ce164-f560-47d0-ab7b-acd423bc6482" />
<img width="1901" height="838" alt="image" src="https://github.com/user-attachments/assets/0bf402fa-adb3-431b-bdd2-88ea66c9d8b2" />
<img width="1905" height="771" alt="image" src="https://github.com/user-attachments/assets/15ab0023-0d6a-468d-862a-4f1f4dda3f74" />
<img width="1909" height="598" alt="image" src="https://github.com/user-attachments/assets/b8220817-3f8e-4852-9a2e-fb3891dbd477" />
<img width="1905" height="823" alt="image" src="https://github.com/user-attachments/assets/53a089a1-365f-4a1a-a240-89d0924cd986" />
<img width="1898" height="818" alt="image" src="https://github.com/user-attachments/assets/cf239670-3269-4d66-9563-44d26c3820b7" />

### Admin Panel
#### Dashboard
<img width="1901" height="919" alt="image" src="https://github.com/user-attachments/assets/fb6e180d-d355-4722-8c05-7ad9580fb76c" />
<img width="1906" height="921" alt="image" src="https://github.com/user-attachments/assets/b0e89acb-2072-44c1-bec3-51b8c6bdec8c" />
<img width="1660" height="817" alt="aidailymenus" src="https://github.com/user-attachments/assets/a26eceb9-6501-4511-b3bf-bc4d877872c8" />

#### Categories
<img width="1901" height="916" alt="image" src="https://github.com/user-attachments/assets/c288aa13-8fd5-4317-8db8-e2213794980c" />

#### Products
<img width="1903" height="921" alt="image" src="https://github.com/user-attachments/assets/c8c07304-0f7b-4084-8941-e890cab8317c" />

#### Features
<img width="1902" height="915" alt="image" src="https://github.com/user-attachments/assets/4f3c167c-dc90-4c88-a216-8cfb54903aed" />

#### About
<img width="1901" height="922" alt="image" src="https://github.com/user-attachments/assets/a6b70dcc-ef12-43a0-a282-8bd9db3d428c" />

#### Reasons to Choose Yummy
<img width="1906" height="915" alt="image" src="https://github.com/user-attachments/assets/6c15d034-d863-4ee0-b7d7-ad43e28d1295" />

#### Testimonials
<img width="1908" height="919" alt="image" src="https://github.com/user-attachments/assets/650c10d5-83ff-4b55-879b-2badfa8258c4" />

#### Special Events
<img width="1905" height="917" alt="image" src="https://github.com/user-attachments/assets/861d1608-ac7d-4716-bff2-1fcc30d30a38" />

#### Chefs
<img width="1903" height="917" alt="image" src="https://github.com/user-attachments/assets/6c54a9d8-c96b-4054-a556-d66c3efd8dc9" />

#### Reservations
<img width="1905" height="918" alt="image" src="https://github.com/user-attachments/assets/2b99890f-0369-4c8c-b0c7-28fcd3011713" />

#### Gallery
<img width="1902" height="915" alt="image" src="https://github.com/user-attachments/assets/63a9ff94-9752-48f7-81ca-f7587a2e3d47" />

#### Contact
<img width="1903" height="922" alt="image" src="https://github.com/user-attachments/assets/39e19802-6ee4-4690-89e5-df7ac42150f8" />

#### Inbox
<img width="1900" height="915" alt="image" src="https://github.com/user-attachments/assets/9e47e2d1-68bc-4fb5-929f-e58cfb04fa11" />

















