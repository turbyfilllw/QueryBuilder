using System;
namespace QueryBuilder
{
	public class ProjectRoot
	{
        public static string Root => Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
    }
}

