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
        public void should_add_a_todo_and_retrieve_it_in_our_list_of_todos()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            todoRepository.Setup(r => r.getAll()).Returns(new List<Todo> { TodoFixture.defaultTodo() });

            var todos = todoService.add("todos", "John Doe");

            todoRepository.Verify(r => r.save(It.IsAny<Todo>()), Times.Once);
            todos.Should().NotBeEmpty();
        }

        [Fact]
        public void should_throw_when_trying_to_add_a_todo_with_blank_title()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            Action act = () => todoService.add("", "John Doe");

            act.Should().Throw<Exception>().WithMessage("Title is mandatory");
        }

        [Fact]
        public void should_throw_when_trying_to_add_a_todo_with_null_title()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            Action act = () => todoService.add(null, "John Doe");

            act.Should().Throw<Exception>().WithMessage("Title is mandatory");
        }

        [Fact]
        public void should_throw_when_trying_to_add_a_todo_that_title_already_match_an_existing_one()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            todoRepository.Setup(r => r.find("Already here")).Returns(TodoFixture.withTitle("Already here"));

            Action act = () => todoService.add("Already here", "John Doe");

            act.Should().Throw<Exception>().WithMessage("'Already here' already exist");
        }

        [Fact]
        public void should_retrieve_todos_ordered_by_created_at_desc()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            var todo1 = TodoFixture.withTitleAndCreatedAt("Todo 1", new DateTime(2025, 4, 23, 10, 25, 0));
            var todo2 = TodoFixture.withTitleAndCreatedAt("Todo 2", new DateTime(2025, 4, 23, 11, 25, 0));
            var todo3 = TodoFixture.withTitleAndCreatedAt("Todo 3", new DateTime(2025, 4, 23, 13, 25, 0));

            todoRepository.Setup(r => r.getAll()).Returns(new List<Todo> { todo1, todo2, todo3 });

            todoService.add("Todo 1", "John Doe");
            todoService.add("Todo 2", "John Doe");
            List<Todo> todos = todoService.add("Todo 3", "John Doe");

            todoRepository.Verify(r => r.save(It.IsAny<Todo>()), Times.Exactly(3));
            todos.Should().ContainInOrder(todo1, todo2, todo3);
        }

        [Fact]
        public void should_mark_a_todo_as_done()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            todoRepository.Setup(r => r.find("Fini")).Returns(TodoFixture.withTitle("Fini"));
            todoRepository.Setup(r => r.getAll()).Returns(new List<Todo> { TodoFixture.withTitleAndDone("Fini") });

            todoService.done("Fini");

            var todoDone = todoService.todos().First(t => t.getTitle() == "Fini");
            todoDone.getTitle().Should().Be("Fini");
            todoDone.getIsDone().Should().BeTrue();
            todoDone.getDoneAt().Should().NotBeNull();
        }

        [Fact]
        public void should_throw_when_trying_to_mark_a_non_existent_todo_as_done()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            todoRepository.Setup(r => r.find("Fini")).Returns((Todo)null);

            Action act = () => todoService.done("Fini");

            act.Should().Throw<Exception>().WithMessage("'Fini' does not exist");
        }

        [Fact]
        public void should_throw_when_author_is_empty()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            Action act = () => todoService.add("Sans auteur", "");

            act.Should().Throw<Exception>().WithMessage("Author is mandatory");
        }

        [Fact]
        public void should_throw_when_author_is_null()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            Action act = () => todoService.add("Sans auteur", null);

            act.Should().Throw<Exception>().WithMessage("Author is mandatory");
        }


        [Fact]
        public void should_have_group_of_todo()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            todoRepository.Setup(r => r.getAll()).Returns(new List<Todo> { TodoFixture.withGroupdId("front todo") });

            var todos = todoService.add("John Doe", "toto", "front todo");

            todos[0].getIdGroup().Should().Be("front todo");
        }

        [Fact]
        public void should_throw_when_trying_to_mark_as_done_todo_already_done()
        {
            TodoService todoService = new TodoService(todoRepository.Object);

            todoRepository.Setup(r => r.find("done")).Returns(TodoFixture.withTitleAndDone("done"));

            Action act = () => todoService.done("done");

            act.Should().Throw<Exception>().WithMessage("'done' is already done");
        }
    }
}
