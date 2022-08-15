namespace BloodCore.Emails
{
    public abstract class EmailTemplate
    {
        public abstract string Subject { get;}
        public abstract string Print();
    }
}
