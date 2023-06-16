Public Module WorldInitializer
    Public Sub Initialize(world As IWorld)
        world.Avatar = GwenInitializer.Initialize(world)
        world.Avatar.GenerateForage()
    End Sub
End Module
