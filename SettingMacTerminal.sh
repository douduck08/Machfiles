# redirct ~/.bash_profile to ~/.bashrc
echo 'if [ -f ~/.bashrc ]; then' >> ~/.bash_profile
echo '    source ~/.bashrc' >> ~/.bash_profile
echo 'fi' >> ~/.bash_profile

# setting ~/.bashrc
echo 'export CLICOLOR="true"' >> ~/.bashrc
echo 'export LSCOLORS="gxfxcxdxcxegedabagacad"' >> ~/.bashrc
echo 'alias ll="ls -al"' >> ~/.bashrc
