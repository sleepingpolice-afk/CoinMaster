package wesleyfrederickoh.wesleyfrederickohUnity.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import wesleyfrederickoh.wesleyfrederickohUnity.dto.SaveDataRequest;
import wesleyfrederickoh.wesleyfrederickohUnity.model.Player;
import wesleyfrederickoh.wesleyfrederickohUnity.model.PlayerUpgrade;
import wesleyfrederickoh.wesleyfrederickohUnity.model.UpgradeType;
import wesleyfrederickoh.wesleyfrederickohUnity.repository.PlayerRepository;
import wesleyfrederickoh.wesleyfrederickohUnity.repository.PlayerUpgradeRepository;
import wesleyfrederickoh.wesleyfrederickohUnity.repository.UpgradeTypeRepository;

import java.util.Map;
import java.util.UUID;

@Service
public class PlayerProgressService {

    @Autowired
    private PlayerRepository playerRepository;

    @Autowired
    private UpgradeTypeRepository upgradeTypeRepository;

    @Autowired
    private PlayerUpgradeRepository playerUpgradeRepository;

    @Transactional
    public void savePlayerProgress(SaveDataRequest saveDataRequest) {
        // Validate playerId
        if (saveDataRequest.getPlayerId() == null) {
            throw new IllegalArgumentException("Player ID is required to save progress.");
        }
        UUID playerId;
        try {
            playerId = UUID.fromString(saveDataRequest.getPlayerId());
        } catch (IllegalArgumentException e) {
            throw new IllegalArgumentException("Invalid Player ID format.");
        }

        Player player = playerRepository.findById(playerId)
                .orElseThrow(() -> new RuntimeException("Player not found with ID: " + saveDataRequest.getPlayerId()));

        player.setCurrency(saveDataRequest.getCurrency());
        playerRepository.save(player);

        if (saveDataRequest.getUpgradeLevels() != null) {
            for (Map.Entry<String, Integer> entry : saveDataRequest.getUpgradeLevels().entrySet()) {
                String upgradeName = entry.getKey();
                Integer level = entry.getValue();

                UpgradeType upgradeType = upgradeTypeRepository.findByName(upgradeName)
                        .orElseThrow(() -> new RuntimeException("UpgradeType not found: " + upgradeName + ". Ensure it is predefined in the database."));

                PlayerUpgrade playerUpgrade = playerUpgradeRepository.findByPlayerAndUpgradeType(player, upgradeType)
                        .orElseGet(() -> new PlayerUpgrade(player, upgradeType, 0)); // Default to level 0 if no prior record

                playerUpgrade.setLevel(level);
                playerUpgradeRepository.save(playerUpgrade);
            }
        }
    }

    @Transactional
    public void decreasePlayerUpgradeLevels(UUID playerId) {
        Player player = playerRepository.findById(playerId)
                .orElseThrow(() -> new RuntimeException("Player not found with ID: " + playerId));

        String[] upgradeNamesToDecrease = {"Production", "Click"};
        int decreaseAmount = 5;

        for (String upgradeName : upgradeNamesToDecrease) {
            UpgradeType upgradeType = upgradeTypeRepository.findByName(upgradeName)
                    .orElseThrow(() -> new RuntimeException("UpgradeType not found: " + upgradeName));

            PlayerUpgrade playerUpgrade = playerUpgradeRepository.findByPlayerAndUpgradeType(player, upgradeType)
                    .orElseGet(() -> {
                        PlayerUpgrade newUpgrade = new PlayerUpgrade(player, upgradeType, 0);
                        playerUpgradeRepository.save(newUpgrade);
                        return newUpgrade;
                    });

            int currentLevel = playerUpgrade.getLevel();
            int newLevel = Math.max(0, currentLevel - decreaseAmount);
            playerUpgrade.setLevel(newLevel);
            playerUpgradeRepository.save(playerUpgrade);
        }
    }
}
