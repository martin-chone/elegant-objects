using Moq;
using FluentAssertions;

namespace ElegantObjects.Core.Tests
{
    public class TodoServiceTest
    {
        Mock<TodoRepository> todoRepository;

        public TodoServiceTest()
        {
            todoRepository = new Mock<TodoRepository>();
        }

        [Fact]
        public void Should_add_a_todo_and_retrieve_it_in_our_list_of_todos()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            todoRepository.Setup(r => r.GetAll()).Returns(new List<Todo> { TodoFixture.DefaultTodo() });

            var todos = todoService.Add("todos", "John Doe");

            todoRepository.Verify(r => r.Save(It.IsAny<Todo>()), Times.Once);
            todos.Should().NotBeEmpty();
        }

        [Fact]
        public void Should_throw_when_trying_to_add_a_todo_with_blank_title()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            Action act = () => todoService.Add("", "John Doe");

            act.Should().Throw<Exception>().WithMessage("Title is mandatory");
        }

        [Fact]
        public void Should_throw_when_trying_to_add_a_todo_with_null_title()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            Action act = () => todoService.Add(null, "John Doe");

            act.Should().Throw<Exception>().WithMessage("Title is mandatory");
        }

        [Fact]
        public void Should_throw_when_trying_to_add_a_todo_that_title_already_match_an_existing_one()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            todoRepository.Setup(r => r.Find("Already here")).Returns(TodoFixture.WithTitle("Already here"));

            Action act = () => todoService.Add("Already here", "John Doe");

            act.Should().Throw<Exception>().WithMessage("'Already here' already exist");
        }

        [Fact]
        public void Should_retrieve_todos_ordered_by_created_at_desc()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            var todo1 = TodoFixture.WithTitleAndCreatedAt("Todo 1", new DateTime(2025, 4, 23, 10, 25, 0));
            var todo2 = TodoFixture.WithTitleAndCreatedAt("Todo 2", new DateTime(2025, 4, 23, 11, 25, 0));
            var todo3 = TodoFixture.WithTitleAndCreatedAt("Todo 3", new DateTime(2025, 4, 23, 13, 25, 0));

            todoRepository.Setup(r => r.GetAll()).Returns(new List<Todo> { todo1, todo2, todo3 });

            todoService.Add("Todo 1", "John Doe");
            todoService.Add("Todo 2", "John Doe");
            List<Todo> todos = todoService.Add("Todo 3", "John Doe");

            todoRepository.Verify(r => r.Save(It.IsAny<Todo>()), Times.Exactly(3));
            todos.Should().ContainInOrder(todo1, todo2, todo3);
        }

        [Fact]
        public void Should_mark_a_todo_as_done()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            todoRepository.Setup(r => r.Find("Fini")).Returns(TodoFixture.WithTitle("Fini"));
            todoRepository.Setup(r => r.GetAll()).Returns(new List<Todo> { TodoFixture.WithTitleAndDone("Fini") });

            todoService.Done("Fini");

            var todoDone = todoService.Todos().First(t => t.Title == "Fini");
            todoDone.Title.Should().Be("Fini");
            todoDone.IsDone.Should().BeTrue();
            todoDone.DoneAt.Should().NotBeNull();
        }

        [Fact]
        public void Should_throw_when_trying_to_mark_a_non_existent_todo_as_done()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            todoRepository.Setup(r => r.Find("Fini")).Returns((Todo)null);

            Action act = () => todoService.Done("Fini");

            act.Should().Throw<Exception>().WithMessage("'Fini' does not exist");
        }

        [Fact]
        public void Should_throw_when_author_is_empty()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            Action act = () => todoService.Add("Sans auteur", "");

            act.Should().Throw<Exception>().WithMessage("Author is mandatory");
        }

        [Fact]
        public void Should_throw_when_author_is_null()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            Action act = () => todoService.Add("Sans auteur", null);

            act.Should().Throw<Exception>().WithMessage("Author is mandatory");
        }


        [Fact]
        public void Should_have_group_of_todo()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            todoRepository.Setup(r => r.GetAll()).Returns(new List<Todo> { TodoFixture.WithGroupdId("front todo") });

            var todos = todoService.Add("John Doe", "toto", "front todo");

            todos[0].IdGroup.Should().Be("front todo");
        }

        [Fact]
        public void Should_throw_when_trying_to_mark_as_done_todo_already_done()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            todoRepository.Setup(r => r.Find("done")).Returns(TodoFixture.WithTitleAndDone("done"));

            Action act = () => todoService.Done("done");

            act.Should().Throw<Exception>().WithMessage("'done' is already done");
        }
    }
}
