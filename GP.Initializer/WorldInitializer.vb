Public Module WorldInitializer
    Public Sub Initialize(world As IWorld)
        world.Avatar = GwenInitializer.Initialize(world)
    End Sub
End Module
