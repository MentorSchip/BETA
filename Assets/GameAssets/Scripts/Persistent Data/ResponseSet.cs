using System;
using System.Collections.Generic;

// Stores list of a student's selected questions and written responses for a single level
// Responsible for adding new entries and checking for duplicates
public class ResponseSet
{
    public ResponseSet(string id)
    {
        this.id = id;
    }

    public string id;
    public List<SingleResponse> values = new List<SingleResponse>();

    public void AddIfNew(string question, string answer)
    {
        if (!ContainsQuestion(question))
            values.Add(new SingleResponse(question, answer));
    }

    public bool ContainsQuestion(string question)
    {
        foreach(var item in values)
            if(item.question == question)
                return true;
        
        return false;
    }
}

// Stores a single question/response
public struct SingleResponse
{
    public SingleResponse(string question, string answer)
    {
        this.question = question;
        this.answer = answer;
    }

    public string question;
    public string answer;
}
