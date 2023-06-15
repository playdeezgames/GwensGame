Friend Class NeutralState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        Dim avatar = Engine.World.Avatar
        If avatar.HasWon Then
            GoToState(GameStates.Win)
            Return
        End If
        If avatar.IsDead Then
            GoToState(GameStates.Dead)
            Return
        End If
        With _frameBuffer
            .BackgroundColor = Black
            .WriteLine()
            .ForegroundColor = Brown
            .WriteLine("Current Status:")
            .ForegroundColor = Gray
            .WriteLine($"You have {avatar.DistanceRemaining} miles left to go!")

            .Write($"Yer health: ")
            .ForegroundColor = GetStatisticColor(avatar.Health, avatar.MaximumHealth)
            .WriteLine($"{avatar.Health}/{avatar.MaximumHealth}")

            .ForegroundColor = Gray
            .Write($"Yer satiety: ")
            .ForegroundColor = GetStatisticColor(avatar.Satiety, avatar.MaximumSatiety)
            .WriteLine($"{avatar.Satiety}/{avatar.MaximumSatiety}")

            .ForegroundColor = Gray
            .WriteLine("1. Keep Going!")
            .WriteLine("0. Game Menu")
        End With
        ShowPrompt()
    End Sub

    Private Function GetStatisticColor(value As Integer, maximum As Integer) As Integer
        Select Case CInt(Math.Ceiling((value / maximum) * 4.0))
            Case Is >= 4
                Return Green
            Case 3
                Return Yellow
            Case 2
                Return Brown
            Case Else
                Return Red
        End Select
    End Function

    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                GoToState(GameStates.GameMenu)
            Case 1
                GoToState(GameStates.KeepGoing)
            Case Else
                Return False
        End Select
        Return True
    End Function
End Class
