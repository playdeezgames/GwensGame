Public Class MainMenuState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                ChangeToState(ApplicationStates.ConfirmQuit)
            Case 1
                ChangeToState(ApplicationStates.Embark)
            Case 2
                ChangeToState(ApplicationStates.Load)
            Case 3
                ChangeToState(ApplicationStates.Options)
            Case Else
                Return False
        End Select
        Return True
    End Function
    Public Overrides Sub Start()
        With _frameBuffer
            .BackgroundColor = Black
            .WriteLine()
            .ForegroundColor = Brown
            .WriteLine("Main Menu:")
            .ForegroundColor = Gray
            .WriteLine("1. Embark!")
            .WriteLine("2. Load...")
            .WriteLine("3. Options...")
            .WriteLine("0. Quit")
        End With
        ShowPrompt()
    End Sub
End Class
