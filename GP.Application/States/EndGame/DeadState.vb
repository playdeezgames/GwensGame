Friend Class DeadState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        With FrameBuffer
            .WriteLine(, LightRed, Black)
            .WriteLine("Yer dead!")
        End With
        Engine.World = Nothing
        GoToState(MainMenu)
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Throw New NotImplementedException()
    End Function
End Class
