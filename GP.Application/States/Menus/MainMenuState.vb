Public Class MainMenuState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                GoToState(GameStates.ConfirmQuit)
            Case 1
                GoToState(GameStates.Embark)
            Case 2
                GoToState(GameStates.Load)
            Case 3
                OptionsState.ExitState = GameStates.MainMenu
                GoToState(GameStates.Options)
            Case Else
                Return False
        End Select
        Return True
    End Function
    Public Overrides Sub Run()
        With FrameBuffer
            .WriteLine(, Brown, Black)
            .WriteLine("Main Menu:")
            .WriteLine("1. Embark!", Gray)
            .WriteLine("2. Load...")
            .WriteLine("3. Options...")
            .WriteLine("0. Quit")
        End With
        ShowPrompt()
    End Sub
End Class
