Friend Class ConfirmAbandonState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        With FrameBuffer
            .WriteLine(, LightRed, Black)
            .WriteLine("Are you sure you want to abandon the game?")
            .WriteLine("1. Yes", Gray)
            .WriteLine("0. No")
        End With
        ShowPrompt()
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                GoToState(GameMenu)
            Case 1
                Engine.World = Nothing
                GoToState(MainMenu)
            Case Else
                Return False
        End Select
        Return True
    End Function
End Class
