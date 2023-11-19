using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    private static Random random = new Random();

    static void Main()
    {
        var scriptureLibrary = new ScriptureLibrary();

    // Adds Bible scriptures
    AddBibleScripture(scriptureLibrary, "John", 3, 16, "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");
    AddBibleScripture(scriptureLibrary, "Proverbs", 3, 5, 6, "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.");
    AddBibleScripture(scriptureLibrary, "Psalm", 23, 1, "The Lord is my shepherd; I shall not want. He maketh me to lie down in green pastures: he leadeth me beside the still waters.");
    AddBibleScripture(scriptureLibrary, "Isaiah", 40, 31, "But they who wait for the Lord shall renew their strength; they shall mount up with wings like eagles; they shall run and not be weary; they shall walk and not faint.");
    AddBibleScripture(scriptureLibrary, "Matthew", 28, 19, "Go therefore and make disciples of all nations, baptizing them in the name of the Father and of the Son and of the Holy Spirit.");

    // Adds Book of Mormon scriptures
    AddBookOfMormonScripture(scriptureLibrary, "2 Nephi", 2, 25, "Adam fell that men might be; and men are, that they might have joy.");
    AddBookOfMormonScripture(scriptureLibrary, "Mosiah", 2, 17, "And behold, I tell you these things that ye may learn wisdom; that ye may learn that when ye are in the service of your fellow beings ye are only in the service of your God.");
    AddBookOfMormonScripture(scriptureLibrary, "Helaman", 5, 12, "And now, my sons, remember, remember that it is upon the rock of our Redeemer, who is Christ, the Son of God, that ye must build your foundation; that when the devil shall send forth his mighty winds, yea, his shafts in the whirlwind, yea, when all his hail and his mighty storm shall beat upon you, it shall have no power over you to drag you down to the gulf of misery and endless wo, because of the rock upon which ye are built, which is a sure foundation, a foundation whereon if men build they cannot fall.");
    AddBookOfMormonScripture(scriptureLibrary, "3 Nephi", 11, 29, "For verily, verily I say unto you, he that hath the spirit of contention is not of me, but is of the devil, who is the father of contention, and he stirreth up the hearts of men to contend with anger, one with another.");
    AddBookOfMormonScripture(scriptureLibrary, "Moroni", 10, 4, "And when ye shall receive these things, I would exhort you that ye would ask God, the Eternal Father, in the name of Christ, if these things are not true; and if ye shall ask with a sincere heart, with real intent, having faith in Christ, he will manifest the truth of it unto you, by the power of the Holy Ghost.");

        do
        {
            var randomScripture = scriptureLibrary.GetRandomScripture();
            Console.WriteLine(randomScripture.GetDisplayText());

            Console.WriteLine("Press Enter to continue or type 'quit' to exit.");
            string userInput = Console.ReadLine();

            if (userInput.Equals("quit", StringComparison.OrdinalIgnoreCase))
                break;

            randomScripture.HideRandomWords(3);
            Console.Clear();

        } while (!scriptureLibrary.AllScripturesCompletelyHidden());
    }

    static void AddBibleScripture(ScriptureLibrary scriptureLibrary, string book, int chapter, int verse, string text)
    {
        scriptureLibrary.AddScripture(new Reference(book, chapter, verse), text);
    }

    static void AddBibleScripture(ScriptureLibrary scriptureLibrary, string book, int chapter, int startVerse, int endVerse, string text)
    {
        scriptureLibrary.AddScripture(new Reference(book, chapter, startVerse, endVerse), text);
    }

    static void AddBookOfMormonScripture(ScriptureLibrary scriptureLibrary, string book, int chapter, int verse, string text)
    {
        scriptureLibrary.AddScripture(new Reference(book, chapter, verse), text);
    }

    static void AddBookOfMormonScripture(ScriptureLibrary scriptureLibrary, string book, int chapter, int startVerse, int endVerse, string text)
    {
        scriptureLibrary.AddScripture(new Reference(book, chapter, startVerse, endVerse), text);
    }

    // Represents a scripture reference
    class Reference
    {
        private string _book;
        private int _chapter;
        private int _verse;
        private int? _endVerse;

        public Reference(string book, int chapter, int verse)
        {
            _book = book;
            _chapter = chapter;
            _verse = verse;
        }

        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            _book = book;
            _chapter = chapter;
            _verse = startVerse;
            _endVerse = endVerse;
        }

        // Get the display text of the reference
        public string GetDisplayText()
        {
            return _endVerse.HasValue ? $"{_book} {_chapter}:{_verse}-{_endVerse}" : $"{_book} {_chapter}:{_verse}";
        }
    }

    // Represents a word in a scripture
    class Word
    {
        private string _text;
        private bool _isHidden;

        public Word(string text)
        {
            _text = text;
        }

        // Hides the word
        public void Hide()
        {
            _isHidden = true;
        }

        // Shows the word
        public void Show()
        {
            _isHidden = false;
        }

        // Checks if the word is hidden
        public bool IsHidden()
        {
            return _isHidden;
        }

        // Get the display text of the word (either the word or "___" if hidden)
        public string GetDisplayText()
        {
            return _isHidden ? "___" : _text;
        }
    }

    // Represents a scripture
    class Scripture
    {
        private Reference _reference;
        private List<Word> _words;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _words = text.Split(' ').Select(word => new Word(word)).ToList();
        }

        // Hide a random number of words in the scripture
        public void HideRandomWords(int numberToHide)
    {
        if (numberToHide < 0 || numberToHide > _words.Count)
        {
            throw new ArgumentException("Invalid number of words to hide.");
        }

        for (int i = 0; i < numberToHide; i++)
        {
            int index = random.Next(0, _words.Count);
            _words[index].Hide();
        }
    }

        // Get the display text of the scripture
        public string GetDisplayText()
        {
            string visibleText = $"{_reference.GetDisplayText()}: ";

            foreach (var word in _words)
            {
                visibleText += $"{word.GetDisplayText()} ";
            }

            return visibleText.Trim();
        }

        // Checks if all words in the scripture are hidden
        public bool IsCompletelyHidden()
        {
            return _words.All(word => word.IsHidden());
        }
    }

    // EXCEEDS EXPECTATIONS BY ADDING A SCRIPTURE LIBRARY

    // A library of scriptures that can be chosen from
    class ScriptureLibrary
    {
        private List<Scripture> _scriptures = new List<Scripture>();

        // Adds a scripture to the library
        public void AddScripture(Reference reference, string text)
        {
            _scriptures.Add(new Scripture(reference, text));
        }

        // Checks if all the scriptures in the library are completely hidden
        public bool AllScripturesCompletelyHidden()
        {
            return _scriptures.All(scripture => scripture.IsCompletelyHidden());
        }

        // Gets a random scripture from the library
        public Scripture GetRandomScripture()
        {
            int index = random.Next(0, _scriptures.Count);
            return _scriptures[index];
        }
    }
}