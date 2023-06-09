Friend Module Forage
    Friend Sub Run(engine As IEngine, context As IContext)
        engine.Prompts.Clear()
        engine.Prompts.Add("You attempt to forage!")
        If context.Abilities.CheckAbility(context.Abilities.GetWisdom(), context.Foraging.Difficulty) Then
            engine.Prompts.Add("Success you gain one snax!")
            context.Snax.Change(1)
            context.Foraging.Abundance -= 1
        Else
            engine.Prompts.Add("You fail!")
        End If
        context.HungerCounter.Change(1)
        engine.ActionItems.Clear()
        engine.ActionItems.Add("Ok", AddressOf Neutral.Run)
    End Sub
End Module
