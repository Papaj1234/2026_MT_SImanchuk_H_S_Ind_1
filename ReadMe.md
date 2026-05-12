# Education task Project example

## English
### General Recommendations
* Every project, whether created in class or independently, must function properly and be configured correctly.  
* Before submitting a project, you must ensure not only that all Unit tests are passed, but also that your code is in order.  
* This means that it must not only be free of compilation errors, but also that all warnings have been resolved.  
* Adding new warnings to the <NoWarn></NoWarn> block (or other equivalent actions) is only permitted with the instructor's approval.  
* The project settings block (*.csproj) specified below cannot be modified except in the cases described above.  
````  
<PropertyGroup>  
<OutputType>Exe</OutputType>
<TargetFramework>NET8.0</TargetFramework>
<EnableNETAnalyzers>true</EnableNETAnalyzers>
<AnalysisMode>AllEnabledByDefault</AnalysisMode>
<CodeAnalysisTreatWarningsAsErrors>false</CodeAnalysisTreatWarningsAsErrors>
<NoWarn>SA0001,SA1633</NoWarn>
</PropertyGroup>
```` 

### Configuring a project based on this repository  
1. Download this project as an archive to your computer or clone the repository (after cloning, proceed immediately to step 4).  
2. Move the archive to the working directory.  
3. Unzip this archive.  
4. Change the name of the **EmptyTask** folder to match the file requirements (e.g. 2026_PT_Ivanov_I_I_Lab_1).  
5. Navigate to the renamed directory.  
6. Go to https://gitlab.com and create an empty repository there, without Readme.md, with the same name as in step 4.  
7. Link the local project to GitLab After creating an empty repository on GitLab (without Readme/License), you will see the instructions page. Select the **HTTP** tab and follow the commands:    
    7.1 **If you downloaded the ZIP archive:** Follow the instructions under **"Push an existing folder"**. This will initialize Git and upload your files.  
    7.2 **If you cloned (git clone) this repository:** First, run git remote remove origin in your terminal to disconnect from the skeleton, then follow the instructions under **"Push an existing Git repository"**   
8. Open the solution and implement the required functionality.  
9. Do not rename the projects inside.  

### Project Structure / Структура проекта / Структура проєкту

**Important:** You must only rename the **root folder**. Do not rename the `.sln` or `.csproj` files inside, as this may break the automated tests.

* **Before (Skeleton):** `emptytask/`
* **After (Submission):** `2026_PT_Ivanov_I_I_Lab_1/`

| File/Folder | Status | Description |
| :--- | :--- | :--- |
| `CodingTask.sln` | ⛔ **DO NOT TOUCH** | Main solution file |
| `CodingTask/` | 📝 Edit Code | Your implementation goes here |
| `CodingTask.Test/` | 🔍 Read/Run | Unit tests (do not modify existing tests) |
| `code-analysis.ruleset` | ⛔ **DO NOT TOUCH** | Static analysis rules |
| `stylecop.json` | ⛔ **DO NOT TOUCH** | StyleCop configuration |
| `.gitignore` | ✅ Keep | Prevents temporary files from being pushed |



## Russian (Русский)  

### Общие рекомендации  
* Каждый проект, который создается на парах или самостоятельно, должен работать надлежащим образом и быть правильно настроен.  
* Перед сдачей проекта Вы должны убедиться не только в том, что у вас выполняются все тесты Юнит, но и в том, что с вашим кодом все в порядке.  
* Это означает, что в нем **не только должны отсутствовать ошибки во время компиляции, но и устранены все варнинги**.  
* Добавление новых варнингов в блок <NoWarn></NoWarn> (или другие эквивалентные действия) исключительно по согласованию с преподавателем.  
* Указанный ниже блок настройки проекта (*.csproj) изменению не подлежит за исключением описаных выше случаев  
````  
<PropertyGroup> 
<OutputType>Exe</OutputType> 
<TargetFramework>NET8.0</TargetFramework> 
<EnableNETAnalyzers>true</EnableNETAnalyzers> 
<AnalysisMode>AllEnabledByDefault</AnalysisMode> 
<CodeAnalysisTreatWarningsAsErrors>false</CodeAnalysisTreatWarningsAsErrors> 
<NoWarn>SA0001,SA1633</NoWarn> 
</PropertyGroup> 
````
### Донастройка проекта на основе этого репозитория  
1. Загружаем этот проект как архив к себе на компьютер или клонируем репозиторий (после клонирования сразу переходим на пункт 4.).  
2. Переносим архив в рабочий каталог  
3. Распаковываем этот архив  
4. Меняем название папки **EmptyTask** в соответствии с требованиями к файлам (например 2026_PT_Ivanov_I_I_Lab_1)  
5. Переходим к переименованному каталогу.
6. Переходим на https://gitlab,.com создаем там пустой репозиторий без Readme.md с таким же названием, что и в пункте 4  
7. Связь локального проекта с GitLab. После создания пустого репозитория на GitLab (без Readme/License) вы попадете на страницу с инструкциями. Выберите вкладку **HTTP** и выполните команды в терминале:
    7.1 **Если вы скачали проект архивом (ZIP):** Выполните команды из блока **"Push an existing folder".** Это инициализирует Git в папке и отправит файлы на сервер.    
    7.2 **Если вы клонировали (git clone) этот репозиторий:** Сначала выполните git remote remove origin, чтобы отвязаться от шаблона, а затем выполните команды из блока **"Push an existing Git repository".**    
8. Открываем решение и реализуем требуемый функционал.  
9. Не переименовываем проекты внутри. 

### Структура проекта  

**Важно:** Вы должны переименовать только **корневую папку**. Не переименовывайте файлы `.sln` или `.csproj` внутри, так как это может нарушить работу автоматических тестов.

* **До (Скелет):** `emptytask/`
* **После (Сдача):** `2026_PT_Ivanov_I_I_Lab_1/`

| Файл/Папка | Статус | Описание |
| :--- | :--- | :--- |
| `CodingTask.sln` | ⛔ **НЕ ТРОГАТЬ** | Главный файл решения |
| `CodingTask/` | 📝 Правка | Здесь вы пишете программный код |
| `CodingTask.Test/` | 🔍 Запуск | Юнит-тесты (не менять существующие) |
| `code-analysis.ruleset` | ⛔ **НЕ ТРОГАТЬ** | Правила статического анализатора |
| `stylecop.json` | ⛔ **НЕ ТРОГАТЬ** | Конфигурация StyleCop |
| `.gitignore` | ✅ Оставить | Исключает мусорные файлы из репозитория |

**Структура каталога до переименования**  
emptytask/                 <-- Исходная папка (ее переименовываем)  
├── .git/                  <-- Скрытая папка Git  
├── .gitignore             <-- Настройки исключений  
├── CodingTask.sln         <-- Файл решения (НЕ переименовывать)  
├── code-analysis.ruleset   <-- Правила анализатора
├── stylecop.json          <-- Конфиг StyleCop  
├── ReadMe.md              <-- Инструкция  
├── CodingTask/            <-- Проект с кодом (НЕ переименовывать)  
│   └── Program.cs  
└── CodingTask.Test/       <-- Проект с тестами (НЕ переименовывать)  
    └── UnitTests.cs  

**Структура каталога после переименования**   
2026_PT_Ivanov_I_I_Lab_1/     <-- Папка переименована по правилу   
├── .git/                  <-- Уже с историей коммитов студента   
├── .gitignore   
├── CodingTask.sln         <-- Имя осталось прежним   
├── code-analysis.ruleset  
├── stylecop.json  
├── ReadMe.md  
├── CodingTask/            <-- Внутри папки студент пишет код   
   └── Program.cs   
└── CodingTask.Test/       <-- Здесь студент запускает тесты   
    └── UnitTests.cs   


## Ukrainian (Українська)

### Загальні рекомендації  
* Кожен проект, який створюється на парах чи самостійно, повинен працювати належним чином та бути правильно налаштований.  
* Перед складанням проекту Ви повинні переконатися не тільки в тому, що у вас виконуються всі тести Юніт, але й у тому, що з вашим кодом все гаразд.  
* Це означає, що в ньому **не тільки повинні бути відсутні помилки під час компіляції, а й усунуті всі варнінги**.  
* Додавання нових варнінгів до блоку <NoWarn></NoWarn> (або інших еквівалентних дій) виключно за погодженням з викладачем.  
* Наведений нижче блок налаштування проекту (*.csproj) зміні не підлягає за винятком описаних вище випадків   
````
<PropertyGroup>
<OutputType>Exe</OutputType>
<TargetFramework>NET8.0</TargetFramework>
<EnableNETAnalyzers>true</EnableNETAnalyzers>
<AnalysisMode>AllEnabledByDefault</AnalysisMode>
<CodeAnalysisTreatWarningsAsErrors>false</CodeAnalysisTreatWarningsAsErrors>
<NoWarn>SA0001,SA1633</NoWarn>
</PropertyGroup>
````


### Доналаштування проекту на основі цього репозиторію  
1. Завантажуємо цей проет як архів до себе на комп'ютер або клонуємо репозиторій (після клонування одразу переходимо на пункт 4.).  
2. Переносимо архів до робочого каталогу  
3. Розпаковуємо цей архив  
4. Міняємо назву папки **EmptyTask** відповідно до вимог до файлів (нариклад 2026_PT_Ivanov_I_I_Lab_1)  
5. Переходимо до переіменованого каталогу.  
6. Переходимо на https://gitlab,.com створюємо там порожній репозиторій без Readme.md з такою самою назвою що і у пункті 4  
7. Зв'язок локального проєкту з GitLab Після створення порожнього репозиторію на GitLab (без Readme/License) ви побачите сторінку з інструкціями. Виберіть вкладку **HTTP** та виконайте команди у терміналі:  
  7.1 **Якщо ви завантажили проєкт архівом (ZIP):** Виконайте команди з блоку **"Push an existing folder".** Це ініціалізує Git у папці та відправить файли на сервер.  
  7.2 **Якщо ви клонували (git clone) цей репозиторій:** Спочатку виконайте команду git remote remove origin, щоб від'єднатися від шаблону, а потім виконайте команди з блоку **"Push an existing Git repository".**  
8. Відкриваємо рішення та реалізуємо протрібний функціонал.  
9. Не переіменовуємо проекти що всередині.  

### Структура проєкту
**Важливо:** Ви маєте перейменувати лише **кореневу папку**. Не перейменовуйте файли `.sln` або `.csproj` всередині, оскільки це може порушити роботу автоматичних тестів.

* **До (Скелет):** `emptytask/`
* **Після (Здача):** `2026_PT_Ivanov_I_I_Lab_1/`

| Файл/Папка | Статус | Опис |
| :--- | :--- | :--- |
| `CodingTask.sln` | ⛔ **НЕ ЧІПАТИ** | Головний файл рішення |
| `CodingTask/` | 📝 Правка | Тут ви пишете програмний код |
| `CodingTask.Test/` | 🔍 Запуск | Юніт-тести (не змінювати існуючі) |
| `code-analysis.ruleset` | ⛔ **НЕ ЧІПАТИ** | Правила статичного аналізатора |
| `stylecop.json` | ⛔ **НЕ ЧІПАТИ** | Конфігурація StyleCop |
| `.gitignore` | ✅ Залишити | Виключає сміттєві файли з репозиторію |
