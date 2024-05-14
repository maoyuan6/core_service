pipeline {
    agent any
    stages {
        stage('pull code') {
            steps {
                git credentialsId: 'maoyuan', url: 'https://github.com/maoyuan6/core_service.git' 
            }
        }
        stage('build project') {
            steps {
               echo 'build project'
               sh 'docker build -f ./Dockerfile -t webapi:core_service .' 
               sh 'docker stop webapi || true' 
               sh 'docker rm -f webapi || true' 
            }
        }
        stage('publish') {
            steps {
                echo 'publish'
                sh 'docker run -d -p 8500:80  --name webapi webapi:core_service'
            }
        }
    }
}
