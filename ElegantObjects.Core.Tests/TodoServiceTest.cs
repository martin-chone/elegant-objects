using Moq;
using FluentAssertions;

namespace ElegantObjects.Core.Tests
{
    public class TodoServiceTest
    {
        private Mock<TodoRepository> _todoRepository;

        public TodoServiceTest()
        {
            _todoRepository = new Mock<TodoRepository>();
        }

        [Fact]
        public void Should_add_a_todo_and_retrieve_it_in_our_list_of_todos()
        {
            TodoService todoService = new TodoService(_todoRepository.Object);

            _todoRepository.Setup(r => r.GetAll()).Returns(new List<Todo> { TodoFixture.DefaultTodo() });

            var title = new Title("todos");
            var author = new Author("John Doe");

            var todos = todoService.Add(title, author);

            _todoRepository.Verify(r => r.Save(It.IsAny<Todo>()), Times.Once);
            todos.Should().NotBeEmpty();
        }

        [Fact]
        public void Should_throw_when_trying_to_add_a_todo_with_blank_title()
        {
            TodoService todoService = new TodoService(_todoRepository.Object);

            var author = new Author("John Doe");

            Action act = () => todoService.Add(new Title(""), author);

            act.Should().Throw<Exception>().WithMessage("Title is mandatory");
        }

        [Fact]
        public void Should_throw_when_trying_to_add_a_todo_with_null_title()
        {
            TodoService todoService = new TodoService(_todoRepository.Object);

            var author = new Author("John Doe");

            Action act = () => todoService.Add(new Title(null), author);

            act.Should().Throw<Exception>().WithMessage("Title is mandatory");
        }

        [Fact]
        public void Should_throw_when_trying_to_add_a_todo_that_title_already_match_an_existing_one()
        {
            TodoService todoService = new TodoService(_todoRepository.Object);

            var title = new Title("todos");
            var author = new Author("John Doe");

            _todoRepository.Setup(r => r.Find(title)).Returns(TodoFixture.WithTitle(title));

            Action act = () => todoService.Add(title, author);

            act.Should().Throw<Exception>().WithMessage("'todos' already exist");
        }

        [Fact]
        public void Should_retrieve_todos_ordered_by_created_at_desc()
        {
            TodoService todoService = new TodoService(_todoRepository.Object);

            var title1 = new Title("Todo 1");
            var title2 = new Title("Todo 2");
            var title3 = new Title("Todo 3");
            var author = new Author("John Doe");

            var todo1 = TodoFixture.WithTitleAndCreatedAt(title1, new DateTime(2025, 4, 23, 10, 25, 0));
            var todo2 = TodoFixture.WithTitleAndCreatedAt(title2, new DateTime(2025, 4, 23, 11, 25, 0));
            var todo3 = TodoFixture.WithTitleAndCreatedAt(title3, new DateTime(2025, 4, 23, 13, 25, 0));

            _todoRepository.Setup(r => r.GetAll()).Returns(new List<Todo> { todo1, todo2, todo3 });

            todoService.Add(title1, author);
            todoService.Add(title2, author);
            List<Todo> todos = todoService.Add(title3, author);

            _todoRepository.Verify(r => r.Save(It.IsAny<Todo>()), Times.Exactly(3));
            todos.Should().ContainInOrder(todo1, todo2, todo3);
        }

        [Fact]
        public void Should_mark_a_todo_as_done()
        {
            TodoService todoService = new TodoService(_todoRepository.Object);

            var title = new Title("Fini");

            _todoRepository.Setup(r => r.Find(title)).Returns(TodoFixture.WithTitle(title));
            _todoRepository.Setup(r => r.GetAll()).Returns(new List<Todo> { TodoFixture.WithTitleAndDone(title) });

            todoService.Done(title);

            var todoDone = todoService.Todos().First(t => t.Title == title);
            todoDone.Title.Should().Be(title);
            todoDone.IsDone.Should().BeTrue();
            todoDone.DoneAt.Should().NotBeNull();
        }

        [Fact]
        public void Should_throw_when_trying_to_mark_a_non_existent_todo_as_done()
        {
            TodoService todoService = new TodoService(_todoRepository.Object);

            var title = new Title("Fini");

            _todoRepository.Setup(r => r.Find(title)).Returns((Todo)null);

            Action act = () => todoService.Done(title);

            act.Should().Throw<Exception>().WithMessage("'Fini' does not exist");
        }

        [Fact]
        public void Should_throw_when_author_is_empty()
        {
            TodoService todoService = new TodoService(_todoRepository.Object);

            var title = new Title("Sans auteur");

            Action act = () => todoService.Add(title, new Author(""));

            act.Should().Throw<Exception>().WithMessage("Author is mandatory");
        }

        [Fact]
        public void Should_throw_when_author_is_null()
        {
            TodoService todoService = new TodoService(_todoRepository.Object);

            var title = new Title("Sans auteur");

            Action act = () => todoService.Add(title, new Author(""));

            act.Should().Throw<Exception>().WithMessage("Author is mandatory");
        }


        [Fact]
        public void Should_have_group_of_todo()
        {
            TodoService todoService = new TodoService(_todoRepository.Object);

            _todoRepository.Setup(r => r.GetAll()).Returns(new List<Todo> { TodoFixture.WithGroupdId("front todo") });

            var title = new Title("John Doe");
            var author = new Author("toto");

            var todos = todoService.Add(author, title, "front todo");

            todos[0].IdGroup.Should().Be("front todo");
        }

        [Fact]
        public void Should_throw_when_trying_to_mark_as_done_todo_already_done()
        {
            TodoService todoService = new TodoService(_todoRepository.Object);

            var title = new Title("done");

            _todoRepository.Setup(r => r.Find(title)).Returns(TodoFixture.WithTitleAndDone(title));

            Action act = () => todoService.Done(title);

            act.Should().Throw<Exception>().WithMessage("'done' is already done");
        }
    }
}
