Friend Class EmbarkState
    Inherits BaseState

    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub

    Public Overrides Sub Run()
        With FrameBuffer
            .WriteLine(, LightCyan, Black)
            .WriteLine("Prolog:")
            .WriteLine("You are Gwen. You seek to get to yer destination, which is quite far away. You", Gray)
            .WriteLine("will get hungry along the way, and you start out with a few snax, but you'll")
            .WriteLine("need to find more if you hope to make it. You will face various challenges and")
            .WriteLine("choices along yer way. Good luck!")
        End With
        Engine.World = World.Create()
        WorldInitializer.Initialize(Engine.World)
        GoToState(GameStates.Neutral)
    End Sub

    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Throw New NotImplementedException("You should never get here!")
    End Function
End Class
