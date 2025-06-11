using Moq;
using FluentAssertions;

namespace ElegantObjects.Core.Tests
{
    public class TodoServiceTest
    {
        private TodoService _todoService;

        public TodoServiceTest()
        {
            _todoService = new TodoService(new TodoRepository());
        }

        [Fact]
        public void Should_add_a_todo_and_retrieve_it_in_our_list_of_todos()
        {
            var todos = _todoService.Add(TitleFixture.Todos(), AuthorFixture.JohnDoe());

            todos.Should().NotBeEmpty();
        }

        [Fact]
        public void Should_throw_when_trying_to_add_a_todo_with_blank_title()
        {
            Action act = () => _todoService.Add(TitleFixture.From(""), AuthorFixture.JohnDoe());

            act.Should().Throw<Exception>().WithMessage("Title is mandatory");
        }

        [Fact]
        public void Should_throw_when_trying_to_add_a_todo_with_null_title()
        {
            Action act = () => _todoService.Add(TitleFixture.From(null), AuthorFixture.JohnDoe());

            act.Should().Throw<Exception>().WithMessage("Title is mandatory");
        }

        [Fact]
        public void Should_throw_when_trying_to_add_a_todo_that_title_already_match_an_existing_one()
        {
            var title = "todos";
            _todoService.Add(TitleFixture.From(title), AuthorFixture.JohnDoe());

            Action act = () => _todoService.Add(TitleFixture.From(title), AuthorFixture.JohnDoe());

            act.Should().Throw<Exception>().WithMessage("'todos' already exist");
        }

        [Fact]
        public void Should_retrieve_todos_ordered_by_created_at_desc()
        {
            var title1 = "Todo 1";
            var title2 = "Todo 2";
            var title3 = "Todo 3";

            _todoService.Add(TitleFixture.From(title1), AuthorFixture.JohnDoe());
            _todoService.Add(TitleFixture.From(title2), AuthorFixture.JohnDoe());
            List<Todo> todos = _todoService.Add(TitleFixture.From(title3), AuthorFixture.JohnDoe());

            var todo1 = TodoFixture.WithTitleAndCreatedAt(title1, new DateTime(2025, 4, 23, 10, 25, 0));
            var todo2 = TodoFixture.WithTitleAndCreatedAt(title2, new DateTime(2025, 4, 23, 11, 25, 0));
            var todo3 = TodoFixture.WithTitleAndCreatedAt(title3, new DateTime(2025, 4, 23, 13, 25, 0));

            todos.Should().ContainInOrder(todo1, todo2, todo3);
        }

        [Fact]
        public void Should_mark_a_todo_as_done()
        {
            var title = TitleFixture.From("Fini");

            _todoService.Add(title, AuthorFixture.JohnDoe());
            _todoService.Done(title);

            var todoDone = _todoService.Todos().First(t => t.Title == title);
            todoDone.Title.Should().Be(title);
            todoDone.Completed.Should().BeTrue();
            todoDone.CompletedAt.Should().NotBeNull();
        }

        [Fact]
        public void Should_throw_when_trying_to_mark_a_non_existent_todo_as_done()
        {
            Action act = () => _todoService.Done(TitleFixture.From("Fini"));

            act.Should().Throw<Exception>().WithMessage("'Fini' does not exist");
        }

        [Fact]
        public void Should_throw_when_author_is_empty()
        {
            Action act = () => _todoService.Add(TitleFixture.Todos(), AuthorFixture.From(""));

            act.Should().Throw<Exception>().WithMessage("Author is mandatory");
        }

        [Fact]
        public void Should_throw_when_author_is_null()
        {
            Action act = () => _todoService.Add(TitleFixture.Todos(), AuthorFixture.From(null));

            act.Should().Throw<Exception>().WithMessage("Author is mandatory");
        }


        [Fact]
        public void Should_have_group_of_todo()
        {
            var todos = _todoService.Add(AuthorFixture.JohnDoe(), TitleFixture.Todos(), "front todo");

            todos[0].IdGroup.Should().Be("front todo");
        }

        [Fact]
        public void Should_throw_when_trying_to_mark_as_done_todo_already_done()
        {
            var title = TitleFixture.Todos();

            _todoService.Add(title, AuthorFixture.JohnDoe());
            _todoService.Done(title);

            Action act = () => _todoService.Done(title);

            act.Should().Throw<Exception>().WithMessage("'todos' is already done");
        }
    }
}
