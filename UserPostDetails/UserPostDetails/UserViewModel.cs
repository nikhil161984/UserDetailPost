using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UserPostDetails
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public List<UserDetail> UserDetails { get; set; }

        public List<DropDownUser> DroupDownUsers { get; set; }

        private UserDetail _userDetail;
        public UserDetail UserDetail
        {
            get { return _userDetail; }
            set
            {
                _userDetail = value;
                OnPropertyChanged("UserDetail");
            }
        }

        private DropDownUser _selectedUser;
        public DropDownUser SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                SelectUser(_selectedUser);
                OnPropertyChanged("SelectedUser");
            }
        }

        public UserViewModel()
        {
            UserDetails = GetUserDetails();
            DroupDownUsers = GetDroupDownUsers();
            SelectUser(DroupDownUsers.FirstOrDefault());
        }

        private List<DropDownUser> GetDroupDownUsers()
        {
            var droupDownUsers = new List<DropDownUser>();
            UserDetails.ForEach(user =>
            {
                DropDownUser dropDownUser = new DropDownUser();
                dropDownUser.DisplyMember = user.user.username;
                dropDownUser.SelectedValue = user.user.id;
                droupDownUsers.Add(dropDownUser);
            });
            return droupDownUsers;
        }

        private List<UserDetail> GetUserDetails()
        {
            var userAPICalls = new UserAPICalls();
            var users = userAPICalls.GetUsers();
            var posts = userAPICalls.GetUserPosts();

            var userDetails = new List<UserDetail>();

            users.ForEach(user =>
            {
                UserDetail userDetail = new UserDetail();
                userDetail.posts = new List<UserPost>();
                userDetail.user = user;
                posts.Where(post => post.userId == user.id).ToList().ForEach(p =>
                {
                    userDetail.posts.Add(new UserPost { Title = p.title, PostDetail = p.body });
                });
                userDetails.Add(userDetail);
            });

            return userDetails;
        }

        private void SelectUser(object param)
        {
            var userDetail = UserDetails.FirstOrDefault(ud => ud.user.id == ((DropDownUser)param).SelectedValue);
            UserDetail = userDetail;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }

    public class DelegateCommand : System.Windows.Input.ICommand
    {
        private Action<object> _action;
        public DelegateCommand(Action<object> action)
        {
            _action = action;
        }

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        #endregion



    }

    public class DropDownUser
    {
        public string DisplyMember { get; set; }
        public int SelectedValue { get; set; }
    }

    public class UserPost
    {
        public string Title { get; set; }
        public string PostDetail { get; set; }
    }
}
