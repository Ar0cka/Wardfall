
## Manual

Ручной Turn Controller должен отслеживать юнита игрока в цепочке юнитов, и передавать действия игроку, если был замечен его юнит. Для этого используется: 

Цепочка Manual Turn Controller

```Scehme
TurnManager -> Turn Controller -> Units Loop -> Find friendly units -> Stop Units Turn -> Player Action -> Await Player Action -> Player Action End -> Units Loop
```

Где:
1. Turn Manager - Определяет какой на данный момент тип контроллера. Сменить тип контроллера можно только в начале нового хода.
2. Turn Controller - управляет цепочкой передачи хода игроку и запускает действия юнитов по их очереди с ожиданием через UniTask
3. Units Loop - цикл, в котором вызываются юниты и проверяется принадлежность юнита к игроку
4. Player Action - начало игрока, происходит по UniTask AwaitPlayerAction чтобы не прерывать цикл и после окончания действия игрока, сразу начинать ход с этого юнита
5. Player Action End - это ивент который отслеживает TurnManager, чтобы уведомить текущий Turn Controller что ход игрока закончен

## Army Turn Controller

Так же является ручным Turn Controller только без полного контроля в течение хода. В начале хода, игроку дается право назначить паттерн на текущий ход, после этого начинается.

Цепочка Army Turn Controller

```Scheme
Player Action -> Turn Manager -> Turn Controller -> Units Loop -> Player Action
```

## 