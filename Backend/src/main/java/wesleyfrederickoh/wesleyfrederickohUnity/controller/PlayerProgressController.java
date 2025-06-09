package wesleyfrederickoh.wesleyfrederickohUnity.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import wesleyfrederickoh.wesleyfrederickohUnity.dto.SaveDataRequest;
import wesleyfrederickoh.wesleyfrederickohUnity.service.PlayerProgressService;

@RestController
@RequestMapping("/api/player-progress")
public class PlayerProgressController {

    @Autowired
    private PlayerProgressService playerProgressService;

    @PostMapping("/save")
    public ResponseEntity<String> savePlayerProgress(@RequestBody SaveDataRequest saveDataRequest) {
        try {
            playerProgressService.savePlayerProgress(saveDataRequest);
            return ResponseEntity.ok("Player progress saved successfully.");
        } catch (Exception e) {
            // Log the exception details here
            return ResponseEntity.status(500).body("Error saving player progress: " + e.getMessage());
        }
    }
}
