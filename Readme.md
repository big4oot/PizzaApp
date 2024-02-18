Тестовое задание на вакансию Junior C# Разработчик

Специально для постоянных покупателей принято решение сделать возможность оплаты заказов с отсрочкой в неделю. 
Клиент в течение недели может заказывать и получать пиццу произвольное количество раз, а оплатить, например, разовым платежом в конце недели. 
При этом возможность заказа при долге свыше недели – блокируется, до поступления оплаты заказа.

Приложение должно быть написано на C# с использованием WinForms или ASP.Net, и обладать следующими возможностями:
1) Ведение справочника клиентов
2) Учет заказов клиентов (для упрощения можно предположить, что клиенты заказывают только одну пиццу за один заказ)
3) Учет оплат за заказы
4) Учет просрочки оплат
При выполнении задания можно пользоваться бесплатными фрейморками из Nuget. Данные хранятся на выбор: в Бд(SqLitel)/файлах/массиве.

Главное условие: приложенный исходный код должен без проблем компилироваться и запускаться из "коробки".

Работа будет оцениваться по следующим параметрам:
1) Реализация приложения со стороны пользователя
* стабильность
* удобство использования
* соответствие ТЗ
2) Качество кода предложенного кандидатом:
* Общая архитектура приложения
* Применённые паттерны


Используемые технологии:
Архитектура: Clean Architecture (https://github.com/jasontaylordev/CleanArchitecture) + MVC
Платформа: ASP NET CORE 8
Entity Framework Core (InMemoryDb, Interseptors)
Automapper
FluentValidation
JQuery Datatables