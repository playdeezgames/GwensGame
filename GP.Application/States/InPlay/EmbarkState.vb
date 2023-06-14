Friend Class EmbarkState
    Inherits BaseState

    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub

    Public Overrides Sub Run()
        Engine.World = World.Create()
        WorldInitializer.Initialize(Engine.World)
        ChangeToState(ApplicationStates.Neutral)
    End Sub

    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Throw New NotImplementedException("You should never get here!")
    End Function
End Class
