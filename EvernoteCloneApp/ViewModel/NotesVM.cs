 using EvernoteCloneApp.Model;
using EvernoteCloneApp.ViewModel.Commands;
using EvernoteCloneApp.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using EvernoteCloneApp.View;
using System.Windows;

namespace EvernoteCloneApp.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                OnPropertyChange("SelectedNotebook");
                GetNotes();
            }
        }

        private Visibility isVisible;
        public Visibility IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChange("IsVisible");
            }
        }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public EditCommand EditCommand { get; set; }
        public EndEditingCommand EndEditingCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);

            EditCommand = new EditCommand(this);
            EndEditingCommand = new EndEditingCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            IsVisible = Visibility.Collapsed
                ;
            GetNotebooks();
        }

        public void CreateNotebook()
        {
            var newNotebook = new Notebook()
            {
                Name = "Новая тетрадь"
            };

            DatabaseHelper.Insert(newNotebook);

            GetNotebooks();
        }

        public void CreateNote(int notebookId)
        {
            var newNote = new Note()
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = "Новая заметка"
            };
            DatabaseHelper.Insert(newNote);

            GetNotes();
        }

        private void GetNotebooks()
        {
            var notebooks = DatabaseHelper.Read<Notebook>();
            Notebooks.Clear();

            foreach (var notebook in notebooks) 
            {
                Notebooks.Add(notebook);
            }
        }

        private void GetNotes()
        {
            if (SelectedNotebook != null)
            {
                var notes = DatabaseHelper.Read<Note>().Where(x => x.NotebookId == SelectedNotebook.Id).ToList();
                Notes.Clear();

                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }

        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
        }

        public void StartEditing()
        {
            IsVisible = Visibility.Visible;
        }

        public void StopEditing(Notebook notebook)
        {
            IsVisible = Visibility.Collapsed;
            DatabaseHelper.Update(notebook);
            GetNotebooks();
        }
    }
}
