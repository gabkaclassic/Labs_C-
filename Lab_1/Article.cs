using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_1
{
    class Article
    {
        public Person Author { get; set; }
        public string Title { get; set; }

        public double Rate { get; set; }

        public Article() {
            Title = "<Empty title>";
            Rate = 0.0;
            Author = new Person();
        }

        public Article(Person author, string title, double rate) {
            Author = author;
            Title = title;
            Rate = rate;
        }

        public override string ToString()
        {
            return $"Article: [Title: {Title}, Rating: {Rate}, Author: {Author}]";
        }
    }
}
