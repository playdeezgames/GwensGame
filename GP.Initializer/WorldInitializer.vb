Public Module WorldInitializer
    Public Sub Initialize(world As IWorld)
        Dim gwen = world.CreateCharacter()
        world.Avatar = gwen
    End Sub
End Module
