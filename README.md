# TestPrototypeProj

Собранный билд под андроид находится в Releases.

Описание проекта:
В проекте используется DI Container, конкретно фреймворк Zenject. 

Использованные ассеты:

https://assetstore.unity.com/packages/vfx/particles/particle-pack-127325

https://assetstore.unity.com/packages/3d/vehicles/low-poly-military-vehicles-package-276939

https://assetstore.unity.com/packages/3d/props/weapons/sci-fi-turrets-cannon-69615

На сцене стоит один SceneContext, в которым расположены все инсталлеры (обьект SYSTEM). Скрипты инсталлеров находятся в папке Scripts/Installer. У каждого инсталлера своя ответственность, выделенная в названии. Основные скрипты, такие как ShootingController, TurretTouchController, WavesManager и др. не являются MonoBehaviour и создаются сразу в контейнере через инсталлеры. Соответственно ссылки на данные классы прокидываются через аттрибут [Inject] перед методом-конструктором (не путать с конструктором класса). Остальные ссылки на обьекты на сцене также прокидываются используя инсталлеры.

Пройдясь коротко по основным скриптам:

TurretTouchController - отвечает за управление турелью

ShootingController - отвечает за стрельбу

WavesManager - отвечает за создание волн (в будуем можно будет выделить дпоолнительный скрипт Level, который будет прокидывать информацию о количестве волн и о количестве врагов в каждой волне)

Bullet - полет пули

LevelUIVisualizer - отвечает за UI

В проекте создан абстрактный класс EnemyBase для создания новых врагов, а также EnemyMoveBase для создания логики передвижения. Соответственно логика передвижения не привязана к конкретному врагу и может переиспользоваться много раз и легко заменяться. Данные классы являются MonoBehaviour, поэтому добавляются к префабу врага-самолету как обычный компонент (но только классы-наследники этих базовых классов).
