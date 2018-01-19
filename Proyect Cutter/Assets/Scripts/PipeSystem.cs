using System.Collections;
using UnityEngine;

public class PipeSystem : MonoBehaviour {

	public Pipe pipePrefab;

	public int pipeCount;

	public int emptyPipeCount;

	private Pipe[] pipes;

    public AudioClip mu_game;

	private void Awake () {
        //Debug.Log("WEH"+emptyPipeCount);
		pipes = new Pipe[pipeCount];
		for (int i = 0; i < pipes.Length; i++) {
			Pipe pipe = pipes[i] = Instantiate<Pipe>(pipePrefab);
            pipe.transform.SetParent(transform, false);
			pipe.Generate(i > emptyPipeCount);
            if (i > 0)
            {
                pipes[i].AlignWith(pipes[i - 1]);
            }
        }
        AlignNextPipeWithOrigin();

        for(int i = 0; i < pipes.Length - 1; i++)
        {
            pipes[i].linkedNextPipe = pipes[i + 1];
        }
	}

    private void Start()
    {
        SoundManager.getInstance().stopMusic();
        SoundManager.getInstance().setMusicVolume(0.3f);
        SoundManager.getInstance().setMusicAndPlay(mu_game);
    }

    public Pipe SetupFirstPipe () {

        //Debug.Log();
		transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
		return pipes[1];
	}

	public Pipe SetupNextPipe () {
		ShiftPipes();
		AlignNextPipeWithOrigin();
		pipes[pipes.Length - 1].Generate();
		pipes[pipes.Length - 1].AlignWith(pipes[pipes.Length - 2]);
		transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
		return pipes[1];
	}

	private void ShiftPipes () {
		Pipe temp = pipes[0];
		for (int i = 1; i < pipes.Length; i++) {
			pipes[i - 1] = pipes[i];
		}
		pipes[pipes.Length - 1] = temp;

        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].name = "Pipe" + i;
            if (i == pipes.Length - 1)
                pipes[i].linkedNextPipe = pipes[0];
            else
                pipes[i].linkedNextPipe = pipes[i + 1];
        }
    }

	private void AlignNextPipeWithOrigin () {
		Transform transformToAlign = pipes[1].transform;
		for (int i = 0; i < pipes.Length; i++) {
			if (i != 1) {
				pipes[i].transform.SetParent(transformToAlign);
			}
		}
		
		transformToAlign.localPosition = Vector3.zero;
		transformToAlign.localRotation = Quaternion.identity;
		
		for (int i = 0; i < pipes.Length; i++) {
			if (i != 1) {
				pipes[i].transform.SetParent(transform);
			}
		}
	}

    public Pipe getClosestPipe(Vector3 fromPosition)
    {
        int pipeID = 0;
        float closest = -1;
        for (int i = 0; i < pipes.Length; i++)
        {
            if (closest == -1 || Vector3.Magnitude(fromPosition - pipes[i].transform.position) < closest)
            {
                closest = Vector3.Magnitude(fromPosition - pipes[i].transform.position);
                pipeID = i;
            }
        }
        return pipes[pipeID];
    }
}