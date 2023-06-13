Friend Class ScreenSizeState
    Inherits BaseState
    Private ReadOnly _setScreenSize As Action(Of Integer)
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer, setScreenSize As Action(Of Integer))
        MyBase.New(stateMachine, frameBuffer)
        _setScreenSize = setScreenSize
    End Sub
    Public Overrides Sub Run()
        With _frameBuffer
            .BackgroundColor = Black
            .WriteLine()
            .ForegroundColor = Brown
            .WriteLine("Display Size:")
            .ForegroundColor = Gray
            For scale = 1 To 12
                .WriteLine($"{scale}. {scale * ScreenWidth}x{scale * ScreenHeight}(x{scale})")
            Next
            .WriteLine("0. Go Back")
        End With
        ShowPrompt()
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 1 To 12
                _setScreenSize(choice)
                Run()
            Case 0
                ChangeToState(Options)
            Case Else
                Return False
        End Select
        Return True
    End Function
End Class
