SimpleValidator
===============

C# SimpleValidator helper for validating models

Usage example: 

<pre><code>
 public class BussinessObjectValidator : ObjectValidatorBase<BussinessObject>
 {
    public BussinessObjectValidator(BussinessObject model)
    {
        Model = model;

        AddRule(o => o.Age >= 18, "User is underage!");
        AddRule(o => !string.IsNullOrEmpty(o.Name), "User is underage!");
        AddRule(o => !string.IsNullOrEmpty(o.Email), "Enter name!");
        AddRule(o => isEMail(o.Email), o => !string.IsNullOrEmpty(o.Email), "Enter valid email!");
    }

    private bool isEMail(string email)
    {
        return true;
    }
 }
 </pre></code>
