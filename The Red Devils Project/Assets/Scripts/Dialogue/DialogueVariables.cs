// ########################################################
// #
// #
// #            Script written by Felix
// #
// #            Referenced and sourced:
// #            Trever Mock - Youtube
// #            
// #
// #
// #
// #
// ########################################################

using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueVariables
{
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    private Story globalVariablesStory;

    private const string saveVariablesKey = "INK_VARIABLES";

    public DialogueVariables(TextAsset loadGlobalsJSON, string globalStateJson)
    {
        // create the story
        globalVariablesStory = new Story(loadGlobalsJSON.text);

        if (!globalStateJson.Equals(""))
        {
            globalVariablesStory.state.LoadJson(globalStateJson);
        }

        variables = new Dictionary<string, Ink.Runtime.Object>();

        // initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
        }
    }

    public string GetGlobalVariablesStateJson()
    {
        // Load the current state of all of our variables to the globals story
        VariablesToStory(globalVariablesStory);
        // return the story variable state
        return globalVariablesStory.state.ToJson();
    }

    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    private void VariableChanged(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

}
