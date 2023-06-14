Friend Class NeutralState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        Dim avatar = Engine.World.Avatar
        With _frameBuffer
            .BackgroundColor = Black
            .WriteLine()
            .ForegroundColor = Brown
            .WriteLine("Current Status:")
            .ForegroundColor = Gray
            .WriteLine($"You have {avatar.DistanceRemaining} miles left to go!")
            .ForegroundColor = Gray
            .WriteLine("1. Keep Going!")
            .WriteLine("0. Game Menu")
        End With
        ShowPrompt()
    End Sub
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
