Friend Class LoadState
    Inherits BaseState

    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub

    Public Overrides Sub Run()
        With _frameBuffer
            .BackgroundColor = Black
            .WriteLine()
            .ForegroundColor = Brown
            .WriteLine("Load From:")
            .ForegroundColor = Gray
            .WriteLine("0. Never Mind")
        End With
    End Sub

    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                ChangeToState(MainMenu)
            Case Else
                Return False
        End Select
        Return True
    End Function
End Class
