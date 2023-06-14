Friend Class GameMenuState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        With _frameBuffer
            .BackgroundColor = Black
            .ForegroundColor = Brown
            .WriteLine()
            .WriteLine("Game Menu:")
            .ForegroundColor = Gray
            .WriteLine("1. Save")
            .WriteLine("2. Abandon Game")
            .WriteLine("3. Options...")
            .WriteLine("0. Resume Game")
        End With
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                ChangeToState(ApplicationStates.Neutral)
            Case 2
                ChangeToState(ApplicationStates.ConfirmAbandon)
            Case Else
                Return False
        End Select
        Return True
    End Function
End Class
