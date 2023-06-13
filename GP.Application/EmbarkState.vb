Friend Class EmbarkState
    Inherits BaseState

    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub

    Public Overrides Sub Start()
        With _frameBuffer
            .ForegroundColor = Gray
            .BackgroundColor = Black
            .WriteLine()
            .WriteLine("Yer so embarked!")
            .WriteLine("0. I know, right?")
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
