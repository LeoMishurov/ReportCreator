# ReportCreator

## Почему?
Выполнение Технического задания

## Функционал

- Создание Excel отчета на основании созданной БД.
  С информацией из таблицы оборудование (вместо ID подразделения должно выводится наименование из таблицы подразделения).

- Добавление записей в бд.

- Редактирование записей в бд.

- Удаление записей из бд.


## Технология
- .NET 7
- Entity Framework
- База данных PostgreSQL
- Шаблон проектирования MVVM
- Клиент на WPF

## Обзор решения 
- ReportCreator.DataAccess – содержит репозитории с методами запростов к базе, модели таблиц бд, и логику взаимодействия с Entity Framework 

- ReportCreator.View – визуальный интерфейс

- ReportCreator.ViewModel - логика взаимодействия клиента WPF с базой, создание и экспорт Excel отчета,
  добавление, удаление и редактирование записей в бд

![image](https://github.com/LeoMishurov/ReportCreator/assets/93088323/d3d1bc4f-cb1b-47dd-bc6d-f0cbc4812e43)
![2](https://github.com/LeoMishurov/ReportCreator/assets/93088323/7c985aaa-0d84-47c3-8b1d-3fc81a369b7c)
![3](https://github.com/LeoMishurov/ReportCreator/assets/93088323/0fa47469-7e34-493c-8ec1-1e49e3149a2a)
![4](https://github.com/LeoMishurov/ReportCreator/assets/93088323/60eb4020-3b08-4bc7-8b18-5ff70eb6842d)
![5](https://github.com/LeoMishurov/ReportCreator/assets/93088323/0c5c667b-70ef-4b0d-a3da-17e7d2d3757c)
![6](https://github.com/LeoMishurov/ReportCreator/assets/93088323/4c691659-5154-4263-8176-f24b5f4401b1)

