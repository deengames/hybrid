using Hybrid.Console.Story;
using Spectre.Console;

namespace Hybrid.Console;

class Program {
    public static void Main(string[] args) {
        new TitleScene().Show();
        new StoryScene().Show();
    }
}