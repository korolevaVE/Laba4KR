# Laba4KR

WPF-приложение: авторизация пользователей и список товаров (добавление, редактирование, удаление, поиск).

## Что нужно установить

- **Windows 10/11**
- **.NET 9 SDK** — https://dotnet.microsoft.com/download (включает рантайм для WPF)
- **SQL Server Express + SSMS** (или любой локальный SQL Server + sqlcmd)
- **Git**

## Подготовка базы данных

1. В SSMS создать базу данных `Lab3_Shop_Makarov`
2. Выполнить скрипт `BD.sql` в контексте этой базы

Или из командной строки:

```bat
sqlcmd -S .\SQLEXPRESS -E -Q "CREATE DATABASE Lab3_Shop_Makarov"
sqlcmd -S .\SQLEXPRESS -E -d Lab3_Shop_Makarov -i BD.sql
```

> Если SQL Server стоит не на `.\SQLEXPRESS`, поменять строку подключения в `Data/ShopDbContext.cs` (метод `OnConfiguring`).

## Запуск

```bat
git clone https://github.com/korolevaVE/Laba4KR.git
cd Laba4KR
dotnet run
```

Либо открыть папку проекта в Visual Studio и нажать F5.

## Учётные записи

| Роль | Логин | Пароль |
|---|---|---|
| Администратор | 94d5ous@gmail.com | uzWC67 |
| Администратор | uth4iz@mail.com | 2L6KZG |
| Администратор | yzls62@outlook.com | JlFRCZ |
| Менеджер | 1diph5e@tutanota.com | 8ntwUp |
| Менеджер | tjde7c@yahoo.com | YOyhfR |
| Менеджер | wpmrc3do@tutanota.com | RSbvHv |
| Авторизированный клиент | 5d4zbu@tutanota.com | rwVDh9 |
| Авторизированный клиент | ptec8ym@yahoo.com | LdNyos |
| Авторизированный клиент | 1qz4kw@mail.com | gynQMT |
| Авторизированный клиент | 4np6se@mail.com | AtnDjr |
