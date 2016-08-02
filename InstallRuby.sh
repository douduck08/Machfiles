# install rbenv
git clone git://github.com/sstephenson/rbenv.git ~/.rbenv

# add settings into ~/.bashrc
echo '# rbenv of Ruby' >> ~/.bashrc
echo 'export PATH="$HOME/.rbenv/bin:$PATH"' >> ~/.bashrc
echo 'eval "$(rbenv init -)"' >> ~/.bashrc
echo 'export RBENV_ROOT=/usr/local/var/rbenv' >> ~/.bashrc
echo 'if which rbenv > /dev/null; then eval "$(rbenv init -)"; fi' >> ~/.bashrc
source .bash_profile

# list available Ruby version
rbenv install -l

# install Ruby
read -p "Enter your wanted version: " version
rbenv install $version
rbenv global $version
rbenv rehash